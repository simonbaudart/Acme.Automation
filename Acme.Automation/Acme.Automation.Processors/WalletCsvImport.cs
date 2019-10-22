// <copyright file="WalletCsvImport.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Import into wallet with a csv.
    /// </summary>
    public class WalletCsvImport : IProcessor, IGroupedProcessor
    {
        /// <inheritdoc />
        public void Execute(JToken config, Message message)
        {
        }

        /// <inheritdoc />
        public void Execute(JToken config, List<Message> messages)
        {
        }
    }
}