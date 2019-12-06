// <copyright file="PaypalEmail.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;
    using Acme.Automation.Core.Models;

    /// <summary>
    /// Convert a paypal email to receipt.
    /// </summary>
    public class PaypalEmail : BaseProcessor<EmptyConfiguration>
    {
        private static readonly Regex PaypalParsing = new Regex("(?<date>\\d+\\s\\w+\\s\\d+\\s\\d+:\\d+:\\d+).*Nº de transaction : (?<reference>\\w+).*Paiement.*€(?<amount>\\d+,\\d+).*Le débit apparaîtra sur votre relevé de carte sous l'intitulé\\s\"(?<creditor>[^\"]*)", RegexOptions.Singleline);

        /// <inheritdoc />
        protected override void Execute(EmptyConfiguration configuration, Message message)
        {
            var textBody = message.Get<string>("textBody");
            var match = PaypalParsing.Match(textBody);

            if (match.Success)
            {
                var culture = CultureInfo.CreateSpecificCulture("fr");

                var transactionInformation = new TransactionInformation();

                transactionInformation.CardName = string.Empty;
                transactionInformation.Category = string.Empty;
                transactionInformation.Note = string.Empty;
                transactionInformation.Currency = "EUR";

                transactionInformation.Amount = decimal.Parse(match.Groups["amount"].Value, NumberStyles.Any, culture);
                transactionInformation.Creditor = match.Groups["creditor"].Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
                transactionInformation.Reference = match.Groups["reference"].Value;
                transactionInformation.UtcDate = DateTime.Parse(match.Groups["date"].Value, culture);

                message.Add(TransactionInformation.MessagePropertyName, transactionInformation);
            }
            else
            {
                this.Log.Warn("Cannot parse the paypal receipt.");
            }
        }
    }
}