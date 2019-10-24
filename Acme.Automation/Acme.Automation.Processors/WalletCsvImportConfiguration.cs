// <copyright file="WalletCsvImportConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for wallet import.
    /// </summary>
    [DataContract]
    public class WalletCsvImportConfiguration
    {
        /// <summary>
        /// Gets or sets the Recipient.
        /// </summary>
        /// <value>The Recipient.</value>
        [DataMember(Name = "recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the Sender.
        /// </summary>
        /// <value>The Sender.</value>
        [DataMember(Name = "sender")]
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the smtp.
        /// </summary>
        [DataMember(Name = "smtp")]
        public SmtpConfiguration Smtp { get; set; }
    }
}