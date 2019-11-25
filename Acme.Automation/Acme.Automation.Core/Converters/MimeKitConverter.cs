// <copyright file="MimeKitConverter.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Converters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Acme.Automation.Core.Models;

    using MimeKit;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Conversions based on MimeKit.
    /// </summary>
    public static class MimeKitConverter
    {
        /// <summary>
        /// Convert to a Message.
        /// </summary>
        /// <param name="senderEmail">The sender email.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="message">The initial message.</param>
        /// <returns>The Acme Message.</returns>
        public static Message ConvertToMessage(MailboxAddress senderEmail, MailboxAddress recipientEmail, MimeMessage message)
        {
            var acmeMessage = new Message();
            acmeMessage.Items.Add("sender", senderEmail.Address);
            acmeMessage.Items.Add("recipient", recipientEmail.Address);
            acmeMessage.Items.Add("date", message.Date);
            acmeMessage.Items.Add("subject", message.Subject);
            acmeMessage.Items.Add("htmlBody", message.HtmlBody);
            acmeMessage.Items.Add("textBody", message.TextBody);

            var attachments = GetAttachments(message);
            acmeMessage.Items.Add("attachments", JToken.FromObject(attachments));

            return acmeMessage;
        }

        private static List<FileData> GetAttachments(MimeMessage message)
        {
            if (message.Attachments == null || !message.Attachments.Any())
            {
                return new List<FileData>();
            }

            var attachments = new List<FileData>();

            foreach (var attachment in message.Attachments)
            {
                if (!(attachment is MimePart attachmentPart))
                {
                    continue;
                }

                var file = new FileData();
                file.FileName = attachmentPart.FileName ?? Guid.NewGuid().ToString();
                file.MediaType = attachment.ContentType.MediaType;
                file.MimeType = attachment.ContentType.MimeType;

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