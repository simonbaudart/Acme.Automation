// <copyright file="IGroupedProcessor.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contract that defines a processor able to manage multiple messages.
    /// </summary>
    public interface IGroupedProcessor
    {
        /// <summary>
        /// Execute the processor with the specified config.
        /// </summary>
        /// <param name="config">The processor specific config.</param>
        /// <param name="messages">The messages to be processed.</param>
        void Execute(JToken config, List<Message> messages);
    }
}