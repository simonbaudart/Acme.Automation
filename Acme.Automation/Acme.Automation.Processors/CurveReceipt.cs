// <copyright file="CurveReceipt.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Models;
    using Acme.Core.Extensions;

    using log4net;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Process the curve receipt and generate fields into the message.
    /// </summary>
    public class CurveReceipt : IProcessor
    {
        private static readonly Regex CurveParsing = new Regex(@"You made a purchase at:\s*(?<Note>.+?)\s+€(?<Amount>\d+\.\d+)\s+(?<Day>\d+)\s+(?<Month>\w+)\s+(?<Year>\d+)\s+(?<Hour>\d+):(?<Minute>\d+):(?<Second>\d+)\s+(?:.\d+\.\d+\s+)?(.+)On this card:\s+(?<Name>.+?)\n\s*(?<CardName>.+)");

        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Worker));

        /// <summary>
        /// The months.
        /// </summary>
        private static readonly Dictionary<string, int> Months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "January", 1 },
            { "February", 2 },
            { "March", 3 },
            { "April", 4 },
            { "May", 5 },
            { "June", 6 },
            { "July", 7 },
            { "August", 8 },
            { "September", 9 },
            { "October", 10 },
            { "November", 11 },
            { "December", 12 },
        };

        /// <inheritdoc />
        public void Execute(JToken config, Message message)
        {
            var textBody = message.Get<string>("textBody");
            var match = CurveParsing.Match(textBody);

            if (match.Success && Months.TryGetValue(match.Groups["Month"].Value, out var month))
            {
                var parsedDate = new DateTime(int.Parse(match.Groups["Year"].Value), month, int.Parse(match.Groups["Day"].Value), int.Parse(match.Groups["Hour"].Value), int.Parse(match.Groups["Minute"].Value), int.Parse(match.Groups["Second"].Value), DateTimeKind.Unspecified);
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                var utcDate = new DateTimeOffset(parsedDate, timeZone.GetUtcOffset(parsedDate)).UtcDateTime;
                var cardName = match.Groups["CardName"].Value.Trim();
                var creditor = match.Groups["Note"].Value;
                var amount = match.Groups["Amount"].Value;

                Log.Info("Adding the transaction informations to the message");

                var transactionInformation = new TransactionInformation();
                transactionInformation.UtcDate = utcDate;
                transactionInformation.CardName = cardName;
                transactionInformation.Creditor = creditor;
                transactionInformation.Note = string.Empty;
                transactionInformation.Amount = -Convert.ToDecimal(amount, CultureInfo.InvariantCulture);
                transactionInformation.Currency = "EUR";
                transactionInformation.Category = string.Empty;
                transactionInformation.Reference = $"{utcDate}-{cardName}-{creditor}-{amount}".SHA512();
                message.Add(TransactionInformation.MessagePropertyName, transactionInformation);
            }
            else
            {
                Log.Warn("Cannot parse the curve receipt.");
            }
        }
    }
}