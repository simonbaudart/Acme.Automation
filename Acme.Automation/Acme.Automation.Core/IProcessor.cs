//  <copyright file="IProcessor.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core
{
    using Newtonsoft.Json.Linq;

    public interface IProcessor
    {
        /// <summary>
        /// Execute the processor with the specified config.
        /// </summary>
        /// <param name="config">The processor specific config.</param>
        /// <param name="message">The message to be processed.</param>
        void Execute(JToken config, Message message);
    }
}