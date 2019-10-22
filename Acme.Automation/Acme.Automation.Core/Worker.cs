//  <copyright file="Worker.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;
    using Acme.Core.Extensions;

    using log4net;

    /// <summary>
    /// Main class for the worker.
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Define the logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Worker));

        /// <summary>
        /// Execute a job.
        /// </summary>
        /// <param name="configuration">The complete configuration.</param>
        /// <param name="job">The job to be executed.</param>
        public void Execute(AutomationConfiguration configuration, Job job)
        {
            Log.Info($"Starting the job : {job.FriendlyName}");

            var connectorConfiguration = configuration.Connectors.SingleOrDefault(x => x.Id == job.Connector) ??
                                         throw new ConfigurationException($"The connector {job.Connector} cannot be found");

            var connector = this.GetConnector(connectorConfiguration);
            var messages = connector.Execute(connectorConfiguration.Config);

            foreach (var action in job.Actions)
            {
                var ruleConfiguration = configuration.Rules.SingleOrDefault(x => x.Id == action.Rule) ??
                                        throw new ConfigurationException($"The connector {action.Rule} cannot be found");

                var processorConfiguration = configuration.Processors.SingleOrDefault(x => x.Id == action.Processor) ??
                                             throw new ConfigurationException($"The processor {action.Processor} cannot be found");

                var rule = this.GetRule(ruleConfiguration);
                var processor = this.GetProcessor(processorConfiguration);

                foreach (var message in messages)
                {
                    if (rule.IsMatch(ruleConfiguration.Config, message))
                    {
                        processor.Execute(processorConfiguration.Config, message);
                    }
                }
            }

            Log.Info($"{messages.Count} retrieved from the connector");

            Log.Info($"Done with the job : {job.FriendlyName}");
        }

        private IProcessor GetProcessor(Processor processorConfiguration)
        {
            processorConfiguration.ThrowIfNull(nameof(processorConfiguration));

            var processorType = Type.GetType(processorConfiguration.Type) ??
                                throw new ConfigurationException($"The processor type {processorConfiguration.Type} cannot be found");

            return Activator.CreateInstance(processorType) as IProcessor ??
                   throw new ConfigurationException($"The processor type {processorConfiguration.Type} does not implement IConnector");
        }

        private IRule GetRule(Rule ruleConfiguration)
        {
            ruleConfiguration.ThrowIfNull(nameof(ruleConfiguration));

            var ruleType = Type.GetType(ruleConfiguration.Type) ??
                           throw new ConfigurationException($"The rule type {ruleConfiguration.Type} cannot be found");

            return Activator.CreateInstance(ruleType) as IRule ??
                   throw new ConfigurationException($"The rule type {ruleConfiguration.Type} does not implement IConnector");
        }

        /// <summary>
        /// Get a connector with reflection.
        /// </summary>
        /// <param name="connectorConfiguration">The connector to be found.</param>
        /// <returns>The connector.</returns>
        private IConnector GetConnector(Connector connectorConfiguration)
        {
            connectorConfiguration.ThrowIfNull(nameof(connectorConfiguration));

            var connectorType = Type.GetType(connectorConfiguration.Type) ?? 
                                throw new ConfigurationException($"The connector type {connectorConfiguration.Type} cannot be found");

            return Activator.CreateInstance(connectorType) as IConnector ??
                   throw new ConfigurationException($"The connector type {connectorConfiguration.Type} does not implement IConnector");
        }
    }
}