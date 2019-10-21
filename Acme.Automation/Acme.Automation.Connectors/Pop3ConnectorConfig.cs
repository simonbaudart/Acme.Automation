//  <copyright file="Pop3ConnectorConfig.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Connectors
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent the configuration for the Pop3Connector.
    /// </summary>
    [DataContract]
    public class Pop3ConnectorConfig
    {
        /// <summary>
        /// Gets or sets the Host.
        /// </summary>
        /// <value>The Host.</value>
        [DataMember(Name = "host")]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>The Password.</value>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Port.
        /// </summary>
        /// <value>The Port.</value>
        [DataMember(Name = "port")]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        /// <value>The UserName.</value>
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the UseSsl.
        /// </summary>
        /// <value>The UseSsl.</value>
        [DataMember(Name = "useSsl")]
        public bool UseSsl { get; set; }
    }
}