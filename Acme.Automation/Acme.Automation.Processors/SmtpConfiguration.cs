// <copyright file="SmtpConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a configuration for a smtp server.
    /// </summary>
    [DataContract]
    public class SmtpConfiguration
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
    }
}