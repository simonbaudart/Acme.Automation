// <copyright file="Pop3Connector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Connectors
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Converters;
    using Acme.Core.Extensions;

    using MailKit.Net.Pop3;

    using MimeKit;

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
                        this.ProcessMails(mail);
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

        private void ProcessMails(MimeMessage message)
        {
            message.ThrowIfNull(nameof(message));

            foreach (var sender in message.From)
            {
                foreach (var recipient in message.To)
                {
                    if (!(sender is MailboxAddress senderEmail) || !(recipient is MailboxAddress recipientEmail))
                    {
                        continue;
                    }

                    var acmeMessage = MimeKitConverter.ConvertToMessage(senderEmail, recipientEmail, message);
                    this.RaiseMessageReceived(acmeMessage);
                }
            }
        }
    }
}