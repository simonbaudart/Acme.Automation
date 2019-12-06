// <copyright file="PaypalReceipt.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;
    using Acme.Automation.Core.Models;

    /// <summary>
    /// Check if the message is a paypal receipt.
    /// </summary>
    public class PaypalReceipt : BaseRule<EmptyConfiguration>
    {
        /// <inheritdoc />
        protected override bool IsMatch(EmptyConfiguration configuration, Message message)
        {
            var subject = message.Get<string>("subject");
            var textBody = message.Get<string>("textBody");

            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(textBody))
            {
                return false;
            }

            return subject.Contains("Reçu pour votre paiement") && textBody.Contains("PayPal (Europe)");
        }
    }
}