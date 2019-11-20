// <copyright file="GenerateMessageStore.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Acme.Automation.Core;

    using log4net;

    using MimeKit;

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

        /// <inheritdoc />
        public override Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, CancellationToken cancellationToken)
        {
            var textMessage = (ITextMessage)transaction.Message;

            var message = MimeKit.MimeMessage.Load(textMessage.Content);

            foreach (var sender in message.From)
            {
                foreach (var recipient in message.To)
                {
                    var senderEmail = sender as MailboxAddress;
                    var recipientEmail = recipient as MailboxAddress;

                    if (senderEmail == null || recipientEmail == null)
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

                    Log.Info($"INCOMING FROM {senderEmail.Address} TO {recipientEmail.Address} : {message.Subject}");
                }
            }

            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}