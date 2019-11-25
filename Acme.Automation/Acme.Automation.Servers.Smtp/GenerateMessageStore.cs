// <copyright file="GenerateMessageStore.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Converters;
    using Acme.Automation.Core.Models;

    using log4net;

    using MimeKit;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using SmtpServer;
    using SmtpServer.Mail;
    using SmtpServer.Protocol;
    using SmtpServer.Storage;

    internal class GenerateMessageStore : MessageStore
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GenerateMessageStore));

        /// <summary>
        /// Handle a message received.
        /// </summary>
        /// <param name="sender">The sender that gets the message.</param>
        /// <param name="message">The message that has been received.</param>
        public delegate void MessageReceivedHandler(object sender, Message message);

        /// <summary>
        /// Event raised when a message is received from the provider.
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <inheritdoc />
        public override Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, CancellationToken cancellationToken)
        {
            var textMessage = (ITextMessage)transaction.Message;

            var message = MimeMessage.Load(textMessage.Content);

            foreach (var sender in message.From)
            {
                foreach (var recipient in message.To)
                {
                    if (!(sender is MailboxAddress senderEmail) || !(recipient is MailboxAddress recipientEmail))
                    {
                        continue;
                    }

                    var acmeMessage = MimeKitConverter.ConvertToMessage(senderEmail, recipientEmail, message);

                    Log.Info($"INCOMING FROM {senderEmail.Address} TO {recipientEmail.Address} : {message.Subject}");

                    this.MessageReceived?.Invoke(this, acmeMessage);
                }
            }

            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}