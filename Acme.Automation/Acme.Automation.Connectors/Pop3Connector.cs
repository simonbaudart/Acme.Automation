// <copyright file="Pop3Connector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using log4net;

    using MailKit;
    using MailKit.Net.Pop3;

    /// <summary>
    /// <see cref="Pop3Connector"/>.
    /// </summary>
    public class Pop3Connector : BaseConnector<Pop3ConnectorConfig>
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Pop3Connector));

        /// <inheritdoc />
        public override List<Message> Execute(Pop3ConnectorConfig configuration)
        {
            Log.Debug($"Fetching the emails from {configuration.Host}:{configuration.Port}");

            var messages = new List<Message>();

            try
            {
                using (var popClient = new Pop3Client(new ProtocolLogger("pop3.txt")))
                {
                    popClient.Connect(configuration.Host, configuration.Port, configuration.UseSsl);
                    popClient.Authenticate(configuration.UserName, configuration.Password);

                    Log.Debug($"Number of available messages = {popClient.Count}");
                    var numberOfMessageToProcess = Math.Min(popClient.Count, 10);
                    if (numberOfMessageToProcess == 0)
                    {
                        Log.Debug("Disconnect from pop without processing messages");
                        popClient.Disconnect(true);
                        return messages;
                    }

                    Log.Debug($"Number of messages to process = {numberOfMessageToProcess}");
                    var mails = popClient.GetMessages(0, numberOfMessageToProcess);

                    foreach (var mail in mails)
                    {
                        var froms = mail.From.Mailboxes.Select(x => x.Address);
                        var tos = mail.To.Mailboxes.Select(x => x.Address);
                        var subject = mail.Subject;
                        var htmlBody = mail.HtmlBody;
                        var textBody = mail.TextBody;
                        var date = mail.Date;

                        messages.AddRange(this.ProcessMails(froms, tos, date, subject, htmlBody, textBody));
                    }

                    Log.Debug($"Deleting message from 0 to {numberOfMessageToProcess}");
                    popClient.DeleteMessages(0, numberOfMessageToProcess);

                    Log.Debug("Disconnect from pop");
                    popClient.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return messages;
        }

        private IEnumerable<Message> ProcessMails(IEnumerable<string> senders, IEnumerable<string> recipients, DateTimeOffset date, string subject, string htmlBody, string textBody)
        {
            var messages = new List<Message>();
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
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}