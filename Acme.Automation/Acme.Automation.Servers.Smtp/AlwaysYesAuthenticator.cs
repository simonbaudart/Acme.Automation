// <copyright file="AlwaysYesAuthenticator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SmtpServer;
    using SmtpServer.Authentication;

    public class AlwaysYesAuthenticator : IUserAuthenticator, IUserAuthenticatorFactory
    {
        public Task<bool> AuthenticateAsync(ISessionContext context, string user, string password, CancellationToken token)
        {
            return Task.FromResult(true);
        }

        public IUserAuthenticator CreateInstance(ISessionContext context)
        {
            return new AlwaysYesAuthenticator();
        }
    }
}