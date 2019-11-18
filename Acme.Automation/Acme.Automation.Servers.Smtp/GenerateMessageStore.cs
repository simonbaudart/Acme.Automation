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

            var messages = new List<Message>();
            foreach (var sender in message.From)
            {
                foreach (var recipient in message.To)
                {
                    var acmeMessage = new Message();
                    acmeMessage.Items.Add("sender", sender.ToString());
                    acmeMessage.Items.Add("recipient", recipient.ToString());
                    acmeMessage.Items.Add("date", message.Date);
                    acmeMessage.Items.Add("subject", message.Subject);
                    acmeMessage.Items.Add("htmlBody", message.HtmlBody);
                    acmeMessage.Items.Add("textBody", message.TextBody);
                    messages.Add(acmeMessage);

                    Log.Info($"INCOMING FROM {sender} TO {recipient} : {message.Subject}");
                }
            }

            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}