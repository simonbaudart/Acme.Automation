// <copyright file="WalletCsvImportConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for wallet import.
    /// </summary>
    [DataContract]
    public class WalletCsvImportConfiguration
    {
        /// <summary>
        /// Gets or sets the EmailAddress.
        /// </summary>
        /// <value>The EmailAddress.</value>
        [DataMember(Name = "emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the smtp.
        /// </summary>
        [DataMember(Name = "smtp")]
        public SmtpConfiguration Smtp { get; set; }
    }
}