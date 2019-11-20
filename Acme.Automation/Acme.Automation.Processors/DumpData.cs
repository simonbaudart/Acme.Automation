// <copyright file="DumpData.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;

    /// <summary>
    /// Processor that dumps on the log all the data from the message.
    /// </summary>
    public class DumpData : BaseProcessor<DumpDataConfiguration>
    {
        /// <inheritdoc />
        protected override void Execute(DumpDataConfiguration configuration, Message message)
        {
            foreach (var item in message.Items)
            {
                if (configuration == null || !configuration.Truncate)
                {
                    this.Log.Info($"{item.Key} : {item.Value}");
                    continue;
                }

                var value = item.Value.ToString();

                var indexOfReturn = value.IndexOf('\n');

                if (indexOfReturn > 0)
                {
                    value = value.Substring(0, indexOfReturn);
                }

                if (value.Length > 250)
                {
                    value = value.Substring(0, 250);
                }

                this.Log.Info($"{item.Key} : {value}");
            }
        }
    }
}