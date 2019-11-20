// <copyright file="BaseRule.cs" company="Acme">
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
    /// Base class for all rules.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    public abstract class BaseRule<TConfiguration> : BaseLoger, IRule
        where TConfiguration : class
    {
        /// <inheritdoc />
        public Rule RuleConfiguration { get; set; }

        /// <inheritdoc />
        public bool IsMatch(Job job, Message message)
        {
            this.Log.Info($"{job.Id} : Checking the rule : {this.RuleConfiguration.Id}");
            return this.IsMatch(this.RuleConfiguration.Config?.ToObject<TConfiguration>(), message);
        }

        /// <summary>
        /// Execute the processor with the specified config.
        /// </summary>
        /// <param name="configuration">The configuration of the processor.</param>
        /// <param name="message">The message to be processed.</param>
        /// <returns>True if the rule matches.</returns>
        protected abstract bool IsMatch(TConfiguration configuration, Message message);
    }
}