// <copyright file="WalletCsvImport.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Models;

    /// <summary>
    /// Import into wallet with a csv.
    /// </summary>
    public class WalletCsvImport : BaseProcessor<WalletCsvImportConfiguration>
    {
        /// <summary>
        /// The format culture.
        /// </summary>
        private static readonly CultureInfo FormatCulture = BuildFormatCulture();

        /// <inheritdoc />
        protected override void Execute(WalletCsvImportConfiguration configuration, Message message)
        {
            var csv = this.BuildHeader();
            this.AppendRow(csv, message);
            this.SendRecords(configuration, csv);
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

            csv.AppendLine($"\"{transaction.Reference?.Replace("\"", "\"\"")}\";\"{transaction.UtcDate:dd/MM/yyyy HH:mm}\";\"{transaction.Note?.Replace("\"", "\"\"")}\";{transaction.Amount:F2};\"{transaction.Currency}\";\"{transaction.Creditor?.Replace("\"", "\"\"")}\";\"{transaction.Category?.Replace("\"", "\"\"")}\"".ToString(FormatCulture));
        }

        private StringBuilder BuildHeader()
        {
            return new StringBuilder("\"Reference\";\"Date\";\"Note\";\"Amount\";\"Currency\";\"Payee\";\"Category\"\r\n");
        }

        private void SendRecords(WalletCsvImportConfiguration configuration, StringBuilder csv)
        {
            using (var csvStream = new MemoryStream(Encoding.Default.GetBytes(csv.ToString())))
            {
                var csvAttachment = new Attachment(csvStream, "records.csv", "text/csv");
                const string Subject = "Update records.";
                var message = new MailMessage
                {
                    From = new MailAddress(configuration.Sender),
                    To = { new MailAddress(configuration.Recipient) },
                    Subject = Subject,
                    Body = Subject,
                    Attachments = { csvAttachment },
                };

                var client = new SmtpClient(configuration.Smtp.Host, configuration.Smtp.Port);

                if (!string.IsNullOrWhiteSpace(configuration.Smtp.UserName))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(configuration.Smtp.UserName, configuration.Smtp.Password);
                }

                client.Send(message);
            }
        }
    }
}