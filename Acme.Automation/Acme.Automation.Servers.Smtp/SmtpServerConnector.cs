// <copyright file="SmtpServerConnector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using log4net;

    using SmtpServer;

    public class SmtpServerConnector
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SmtpServerConnector));

        private SmtpServer smtpServer;

        /// <summary>
        /// Gets the Ports.
        /// </summary>
        /// <value>The Ports.</value>
        public int[] Ports { get; }

        /// <summary>
        /// Gets the ServerName.
        /// </summary>
        /// <value>The ServerName.</value>
        public string ServerName { get; }

        public SmtpServerConnector(string serverName, params int[] ports)
        {
            this.ServerName = serverName;
            this.Ports = ports;
        }

        /// <summary>
        /// Start a new server.
        /// </summary>
        /// <returns>The task to wait for</returns>
        public async Task Start()
        {
            Log.Info($"STARTING NEW SMTP SERVER ON {this.ServerName}:{string.Join(",", this.Ports)}");

            var options = new SmtpServerOptionsBuilder()
                .ServerName(this.ServerName)
                .Port(this.Ports)
                .MessageStore(new GenerateMessageStore())
                .MailboxFilter(new AlwaysYesMailboxFilter())
                .UserAuthenticator(new AlwaysYesAuthenticator())
                .Build();

            this.smtpServer = new SmtpServer(options);
            await this.smtpServer.StartAsync(CancellationToken.None);
        }
    }
}