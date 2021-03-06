// <copyright file="BaseProcessor.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;

    using log4net;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Base class for all processor.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    public abstract class BaseProcessor<TConfiguration> : BaseLoger, IProcessor
        where TConfiguration : class
    {
        /// <inheritdoc />
        public Processor ProcessorConfiguration { get; set; }

        /// <inheritdoc />
        public void Execute(Job job, Message message)
        {
            try
            {
                this.Log.Info($"{job.Id} : Running the processor id : {this.ProcessorConfiguration.Id}");
                this.Execute(this.ProcessorConfiguration.Config?.ToObject<TConfiguration>(), message);
            }
            catch (Exception e)
            {
                this.Log.Error($"{job.Id} : Running the processor id : {this.ProcessorConfiguration.Id}", e);
            }
        }

        /// <summary>
        /// Execute the processor with the specified config.
        /// </summary>
        /// <param name="configuration">The configuration of the processor.</param>
        /// <param name="message">The message to be processed.</param>
        protected abstract void Execute(TConfiguration configuration, Message message);
    }
}