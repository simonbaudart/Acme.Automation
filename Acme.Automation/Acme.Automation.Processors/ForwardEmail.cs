// <copyright file="ForwardEmail.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Models;

    /// <summary>
    /// Forward an email.
    /// </summary>
    public class ForwardEmail : BaseProcessor<ForwardEmailConfiguration>
    {
        /// <inheritdoc />
        protected override void Execute(ForwardEmailConfiguration configuration, Message message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration.Sender),
                To = { new MailAddress(configuration.Recipient) },
                Subject = message.Get<string>("subject"),
                Body = $"Message forwarded from {message.Items["sender"]} to {message.Items["recipient"]}<hr />\r\n\r\n<br /><br />" + (message.Get<string>("htmlBody") ?? message.Get<string>("textBody")),
                IsBodyHtml = true,
            };

            if (message.Items.ContainsKey("attachments"))
            {
                var attachments = message.Get<List<FileData>>("attachments");
                if (attachments != null)
                {
                    foreach (var fileData in attachments)
                    {
                        var attachment = new Attachment(new MemoryStream(Convert.FromBase64String(fileData.Base64Content)), fileData.FileName, fileData.ContentType);
                        mailMessage.Attachments.Add(attachment);
                    }
                }
            }

            var client = new SmtpClient(configuration.Smtp.Host, configuration.Smtp.Port);

            if (!string.IsNullOrWhiteSpace(configuration.Smtp.UserName))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(configuration.Smtp.UserName, configuration.Smtp.Password);
            }

            client.Send(mailMessage);
        }
    }
}