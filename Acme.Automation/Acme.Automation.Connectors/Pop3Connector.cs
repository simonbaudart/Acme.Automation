// <copyright file="Pop3Connector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using MailKit.Net.Pop3;

    /// <summary>
    /// <see cref="Pop3Connector" />.
    /// </summary>
    public class Pop3Connector : BaseConnector<Pop3ConnectorConfig>
    {
        /// <inheritdoc />
        protected override void Execute(Pop3ConnectorConfig configuration)
        {
            this.Log.Debug($"Fetching the emails from {configuration.Host}:{configuration.Port}");

            try
            {
                using (var popClient = new Pop3Client())
                {
                    popClient.Connect(configuration.Host, configuration.Port, configuration.UseSsl);
                    popClient.Authenticate(configuration.UserName, configuration.Password);

                    this.Log.Debug($"Number of available messages = {popClient.Count}");
                    var numberOfMessageToProcess = Math.Min(popClient.Count, 10);
                    if (numberOfMessageToProcess == 0)
                    {
                        this.Log.Debug("Disconnect from pop without processing messages");
                        popClient.Disconnect(true);
                        return;
                    }

                    this.Log.Debug($"Number of messages to process = {numberOfMessageToProcess}");
                    var mails = popClient.GetMessages(0, numberOfMessageToProcess);

                    foreach (var mail in mails)
                    {
                        var froms = mail.From.Mailboxes.Select(x => x.Address);
                        var tos = mail.To.Mailboxes.Select(x => x.Address);
                        var subject = mail.Subject;
                        var htmlBody = mail.HtmlBody;
                        var textBody = mail.TextBody;
                        var date = mail.Date;

                        this.ProcessMails(froms, tos, date, subject, htmlBody, textBody);
                    }

                    this.Log.Debug($"Deleting message from 0 to {numberOfMessageToProcess}");
                    popClient.DeleteMessages(0, numberOfMessageToProcess);

                    this.Log.Debug("Disconnect from pop");
                    popClient.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                this.Log.Error(e);
            }
        }

        private void ProcessMails(IEnumerable<string> senders, IEnumerable<string> recipients, DateTimeOffset date, string subject, string htmlBody, string textBody)
        {
            var recipientsList = recipients.ToList();

            foreach (var sender in senders)
            {
                foreach (var recipient in recipientsList)
                {
                    var message = new Message();
                    message.Items.Add("sender", sender);
                    message.Items.Add("recipient", recipient);
                    message.Items.Add("date", date);
                    message.Items.Add("subject", subject);
                    message.Items.Add("htmlBody", htmlBody);
                    message.Items.Add("textBody", textBody);

                    this.RaiseMessageReceived(message);
                }
            }
        }
    }
}