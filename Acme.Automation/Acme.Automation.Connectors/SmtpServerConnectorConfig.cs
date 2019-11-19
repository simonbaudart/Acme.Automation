// <copyright file="SmtpServerConnectorConfig.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Connectors
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for smtp server.
    /// </summary>
    [DataContract]
    public class SmtpServerConnectorConfig
    {
        /// <summary>
        /// Gets or sets the ServerName.
        /// </summary>
        /// <value>The ServerName.</value>
        [DataMember(Name = "serverName")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Ports.
        /// </summary>
        /// <value>The Ports.</value>
        [DataMember(Name = "ports")]
        public int[] Ports { get; set; }
    }
}