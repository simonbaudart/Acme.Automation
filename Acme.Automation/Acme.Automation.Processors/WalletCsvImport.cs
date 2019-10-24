// <copyright file="WalletCsvImport.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Models;

    using log4net;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Import into wallet with a csv.
    /// </summary>
    public class WalletCsvImport : IProcessor, IGroupedProcessor
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(WalletCsvImport));

        /// <summary>
        /// The format culture.
        /// </summary>
        private static readonly CultureInfo FormatCulture = BuildFormatCulture();

        /// <inheritdoc />
        public void Execute(JToken config, Message message)
        {
            var configWalletCsvImport = config.ToObject<WalletCsvImportConfiguration>();

            var csv = this.BuildHeader();
            this.AppendRow(csv, message);
            this.SendRecords(configWalletCsvImport, csv);
        }

        /// <inheritdoc />
        public void Execute(JToken config, List<Message> messages)
        {
            if (messages.Count == 0)
            {
                Log.Info("No message in list, skipping CSV");
                return;
            }

            var configWalletCsvImport = config.ToObject<WalletCsvImportConfiguration>();

            var csv = this.BuildHeader();
            messages.ForEach(x => this.AppendRow(csv, x));
            this.SendRecords(configWalletCsvImport, csv);
        }

        /// <summary>
        /// Builds the format culture.
        /// </summary>
        /// <returns>The culture to be used in csv.</returns>
        private static CultureInfo BuildFormatCulture()
        {
            var result = CultureInfo.CreateSpecificCulture("fr");
            result.DateTimeFormat.DateSeparator = "/";
            return result;
        }

        private void AppendRow(StringBuilder csv, Message message)
        {
            var transaction = message.Get<TransactionInformation>(TransactionInformation.MessagePropertyName);

            csv.AppendLine($"\"{transaction.Reference}\";\"{transaction.UtcDate:dd/MM/yyyy HH:mm}\";\"{transaction.Note.Replace("\"", "\"\"")}\";{transaction.Amount:F2};\"{transaction.Currency}\";\"{transaction.Creditor}\";\"{transaction.Category}\"".ToString(FormatCulture));
        }

        private StringBuilder BuildHeader()
        {
            return new StringBuilder("\"Reference\";\"Date\";\"Note\";\"Amount\";\"Currency\";\"Payee\";\"Category\"\r\n");
        }

        private void SendRecords(WalletCsvImportConfiguration configWalletCsvImport, StringBuilder csv)
        {
            using (var csvStream = new MemoryStream(Encoding.Default.GetBytes(csv.ToString())))
            {
                var csvAttachment = new Attachment(csvStream, "records.csv", "text/csv");
                const string Subject = "Update records.";
                var message = new MailMessage
                {
                    From = new MailAddress(configWalletCsvImport.Sender),
                    To = { new MailAddress(configWalletCsvImport.Recipient) },
                    Subject = Subject,
                    Body = Subject,
                    Attachments = { csvAttachment },
                };

                var client = new SmtpClient(configWalletCsvImport.Smtp.Host, configWalletCsvImport.Smtp.Port);

                if (!string.IsNullOrWhiteSpace(configWalletCsvImport.Smtp.UserName))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(configWalletCsvImport.Smtp.UserName, configWalletCsvImport.Smtp.Password);
                }

                client.Send(message);
            }
        }
    }
}