// <copyright file="SmtpServerListener.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Acme.Automation.Core;

    using SmtpServer;

    public class SmtpServerListener : BaseLoger
    {
        private SmtpServer _smtpServer;

        public SmtpServerListener(string serverName, params int[] ports)
        {
            this.ServerName = serverName;
            this.Ports = ports;
        }

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

        /// <summary>
        /// Gets the Ports.
        /// </summary>
        /// <value>The Ports.</value>
        private int[] Ports { get; }

        /// <summary>
        /// Gets the ServerName.
        /// </summary>
        /// <value>The ServerName.</value>
        private string ServerName { get; }

        /// <summary>
        /// Start a new server.
        /// </summary>
        /// <returns>The task to wait for</returns>
        public async Task Start()
        {
            this.Log.Info($"STARTING NEW SMTP SERVER ON {this.ServerName}:{string.Join(",", this.Ports)}");

            var generateMessageStore = new GenerateMessageStore();
            generateMessageStore.MessageReceived += (sender, message) => { this.MessageReceived?.Invoke(this, message); };

            var options = new SmtpServerOptionsBuilder()
                .ServerName(this.ServerName)
                .Port(this.Ports)
                .MessageStore(generateMessageStore)
                .MailboxFilter(new AlwaysYesMailboxFilter())
                .UserAuthenticator(new AlwaysYesAuthenticator())
                .Build();

            this._smtpServer = new SmtpServer(options);
            await this._smtpServer.StartAsync(CancellationToken.None);
        }
    }
}