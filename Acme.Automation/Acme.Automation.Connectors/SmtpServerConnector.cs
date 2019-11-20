// <copyright file="SmtpServerConnector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Connectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Servers.Smtp;

    /// <summary>
    /// Start a new Smtp Server and listen in background.
    /// </summary>
    public class SmtpServerConnector : BaseConnector<SmtpServerConnectorConfig>
    {
        private static SmtpServerListener listener;

        /// <inheritdoc />
        protected override void Execute(SmtpServerConnectorConfig configuration)
        {
            if (listener == null)
            {
                listener = new SmtpServerListener(configuration.ServerName, configuration.Ports);
                listener.Start();
            }
        }
    }
}