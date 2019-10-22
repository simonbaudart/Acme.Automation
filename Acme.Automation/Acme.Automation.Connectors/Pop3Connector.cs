//  <copyright file="Pop3Connector.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using log4net;

    using MailKit;
    using MailKit.Net.Pop3;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// </summary>
    public class Pop3Connector : IConnector
    {
        /// <summary>
        /// Define the logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Pop3Connector));

        /// <summary>
        /// Execute the pop3 worker.
        /// </summary>
        /// <param name="config">The config for the connector.</param>
        /// <returns>List of all messages.</returns>
        public List<Message> Execute(JToken config)
        {
            Log.Info("START THE POP3 WORKER");

            var pop3Settings = config.ToObject<Pop3ConnectorConfig>();
            var emails = this.FetchEmails(pop3Settings);

            Log.Info("END THE POP3 WORKER");

            return emails;
        }

        private List<Message> FetchEmails(Pop3ConnectorConfig pop3Settings)
        {
            Log.Debug($"Fetching the emails from {pop3Settings.Host}:{pop3Settings.Port}");

            var messages = new List<Message>();

            try
            {
                using (var popClient = new Pop3Client(new ProtocolLogger("pop3.txt")))
                {
                    popClient.Connect(pop3Settings.Host, pop3Settings.Port, pop3Settings.UseSsl);
                    popClient.Authenticate(pop3Settings.UserName, pop3Settings.Password);

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
                        var body = mail.HtmlBody ?? mail.TextBody;
                        var date = mail.Date;

                        messages.AddRange(this.ProcessMails(froms, tos, date, subject, body));
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

        private IEnumerable<Message> ProcessMails(IEnumerable<string> senders, IEnumerable<string> recipients, DateTimeOffset date, string subject, string body)
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
                    message.Items.Add("body", body);
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}