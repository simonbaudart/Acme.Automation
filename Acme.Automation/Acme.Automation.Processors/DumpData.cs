// <copyright file="DumpData.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using log4net;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Processor that dumps on the log all the data from the message.
    /// </summary>
    public class DumpData : IProcessor, IGroupedProcessor
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DumpData));

        /// <inheritdoc />
        public void Execute(JToken config, Message message)
        {
            foreach (var item in message.Items)
            {
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

                Log.Info($"{item.Key} : {value}");
            }
        }

        /// <inheritdoc />
        public void Execute(JToken config, List<Message> messages)
        {
            foreach (var message in messages)
            {
                this.Execute(config, message);
            }
        }
    }
}