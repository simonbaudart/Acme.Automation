// <copyright file="TransactionInformation.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents informations about a transaction.
    /// Used to be stored in message data.
    /// </summary>
    [DataContract]
    public class TransactionInformation
    {
        /// <summary>
        /// Gets the name of the property in the message.
        /// </summary>
        public const string MessagePropertyName = "transaction";

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the CardName.
        /// </summary>
        /// <value>The CardName.</value>
        [DataMember(Name = "cardName")]
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the Category.
        /// </summary>
        /// <value>The Category.</value>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the Creditor.
        /// </summary>
        /// <value>The Creditor.</value>
        [DataMember(Name = "creditor")]
        public string Creditor { get; set; }

        /// <summary>
        /// Gets or sets the Currency.
        /// </summary>
        /// <value>The Currency.</value>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the Note.
        /// </summary>
        /// <value>The Note.</value>
        [DataMember(Name = "note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the Reference.
        /// </summary>
        /// <value>The Reference.</value>
        [DataMember(Name = "reference")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the UtcDate.
        /// </summary>
        /// <value>The UtcDate.</value>
        [DataMember(Name = "utcDate")]
        public DateTime UtcDate { get; set; }
    }
}