// <copyright file="AlwaysYesMailboxFilter.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SmtpServer;
    using SmtpServer.Mail;
    using SmtpServer.Storage;

    internal class AlwaysYesMailboxFilter : IMailboxFilter, IMailboxFilterFactory
    {
        public Task<MailboxFilterResult> CanAcceptFromAsync(ISessionContext context, IMailbox from, int size, CancellationToken token)
        {
            return Task.FromResult(MailboxFilterResult.Yes);
        }

        public Task<MailboxFilterResult> CanDeliverToAsync(ISessionContext context, IMailbox to, IMailbox from, CancellationToken token)
        {
            return Task.FromResult(MailboxFilterResult.Yes);
        }

        public IMailboxFilter CreateInstance(ISessionContext context)
        {
            return new AlwaysYesMailboxFilter();
        }
    }
}