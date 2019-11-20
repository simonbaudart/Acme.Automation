// <copyright file="IProcessor.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contract for a processor.
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// Gets or sets the processor configuration.
        /// </summary>
        Processor ProcessorConfiguration { get; set; }

        /// <summary>
        /// Execute the processor with the specified config.
        /// </summary>
        /// <param name="job">The source job.</param>
        /// <param name="message">The message to be processed.</param>
        void Execute(Job job, Message message);
    }
}