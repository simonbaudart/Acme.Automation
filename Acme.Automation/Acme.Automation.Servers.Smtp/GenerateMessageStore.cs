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
    using Acme.Automation.Core.Models;

    using log4net;

    using MimeKit;

    using Newtonsoft.Json;

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

                    var acmeMessage = new Message();
                    acmeMessage.Items.Add("sender", senderEmail.Address);
                    acmeMessage.Items.Add("recipient", recipientEmail.Address);
                    acmeMessage.Items.Add("date", message.Date);
                    acmeMessage.Items.Add("subject", message.Subject);
                    acmeMessage.Items.Add("htmlBody", message.HtmlBody);
                    acmeMessage.Items.Add("textBody", message.TextBody);

                    var attachments = this.GetAttachments(message);
                    acmeMessage.Items.Add("attachments", JsonConvert.SerializeObject(attachments));

                    Log.Info($"INCOMING FROM {senderEmail.Address} TO {recipientEmail.Address} : {message.Subject}");

                    this.MessageReceived?.Invoke(this, acmeMessage);
                }
            }

            return Task.FromResult(SmtpResponse.Ok);
        }

        private List<FileData> GetAttachments(MimeMessage message)
        {
            if (message.Attachments == null || !message.Attachments.Any())
            {
                return null;
            }

            var attachments = new List<FileData>();

            foreach (var attachment in message.Attachments)
            {
                var file = new FileData();
                file.FileName = attachment.ContentDisposition?.FileName ?? Guid.NewGuid().ToString();
                file.ContentType = attachment.ContentType.ToString();

                using (var memoryAttachment = new MemoryStream())
                {
                    attachment.WriteTo(memoryAttachment);
                    file.Base64Content = Convert.ToBase64String(memoryAttachment.ToArray());
                }

                attachments.Add(file);
            }

            return attachments;
        }
    }
}