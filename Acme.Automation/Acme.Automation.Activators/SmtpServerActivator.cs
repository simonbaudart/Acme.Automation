// <copyright file="SmtpServerActivator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Activators
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Models;
    using Acme.Automation.Servers.Smtp;

    /// <summary>
    /// Start a new Smtp Server and listen in background.
    /// </summary>
    public class SmtpServerActivator : BaseActivator<SmtpServerConnectorConfig>
    {
        private static SmtpServerListener _listener;

        protected override void Start(SmtpServerConnectorConfig config)
        {
            this.StartServer(config);
        }

        protected override void WakeUp(SmtpServerConnectorConfig config)
        {
            this.StartServer(config);
        }

        private void StartServer(SmtpServerConnectorConfig config)
        {
            if (_listener != null)
            {
                return;
            }

            this.Log.Info($"The SMTP server is not running, starting {config.ServerName} on {string.Join(",", config.Ports)}");
            _listener = new SmtpServerListener(config.ServerName, config.Ports);
            _listener.MessageReceived += (sender, message) => { this.RaiseMessageReceived(message); };
            var task = _listener.Start();
            task.ConfigureAwait(false);
        }
    }
}