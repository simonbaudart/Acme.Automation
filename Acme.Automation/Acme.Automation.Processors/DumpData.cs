// <copyright file="DumpData.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;

    using log4net;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Processor that dumps on the log all the data from the message.
    /// </summary>
    public class DumpData : IProcessor
    {
        /// <summary>
        /// Define the logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DumpData));

        /// <inheritdoc />
        public void Execute(JToken config, Message message)
        {
            foreach (var item in message.Items)
            {
                Log.Info($"{item.Key} : {item.Value}");
            }
        }
    }
}