// <copyright file="Worker.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Collections.Generic;
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
        /// Define the logger.
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

            Log.Debug("Start executing actions");
            this.ExecuteActions(messages, configuration, job);

            Log.Debug("Start executing grouped actions");
            this.ExecuteGroupedActions(messages, configuration, job);

            Log.Info($"{messages.Count} retrieved from the connector");

            Log.Info($"Done with the job : {job.FriendlyName}");
        }

        private void ExecuteActions(IReadOnlyCollection<Message> messages, AutomationConfiguration configuration, Job job)
        {
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
                    var ruleMatch = rule.IsMatch(ruleConfiguration.Config, message);
                    Log.Debug($"The rule {ruleConfiguration.FriendlyName} returns : {ruleMatch}");

                    if (!ruleMatch)
                    {
                        continue;
                    }

                    Log.Info($"Executing the processor on one message : {processorConfiguration.FriendlyName}");
                    processor.Execute(processorConfiguration.Config, message);
                }
            }
        }

        private void ExecuteGroupedActions(IReadOnlyCollection<Message> messages, AutomationConfiguration configuration, Job job)
        {
            foreach (var action in job.GroupedActions)
            {
                var ruleConfiguration = configuration.Rules.SingleOrDefault(x => x.Id == action.Rule) ??
                                        throw new ConfigurationException($"The connector {action.Rule} cannot be found");

                var processorConfiguration = configuration.Processors.SingleOrDefault(x => x.Id == action.Processor) ??
                                             throw new ConfigurationException($"The processor {action.Processor} cannot be found");

                var rule = this.GetRule(ruleConfiguration);
                var processor = this.GetGroupedProcessor(processorConfiguration);

                var matchingMessages = messages.Where(x => rule.IsMatch(ruleConfiguration.Config, x)).ToList();
                Log.Debug($"The rule {ruleConfiguration.FriendlyName} matches on {matchingMessages.Count} messages");

                if (matchingMessages.Count <= 0)
                {
                    continue;
                }

                Log.Info($"Executing the processor on {matchingMessages.Count} messages : {processorConfiguration.FriendlyName}");
                processor.Execute(processorConfiguration.Config, matchingMessages);
            }
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

        private IGroupedProcessor GetGroupedProcessor(Processor processorConfiguration)
        {
            processorConfiguration.ThrowIfNull(nameof(processorConfiguration));

            var processorType = Type.GetType(processorConfiguration.Type) ??
                                throw new ConfigurationException($"The processor type {processorConfiguration.Type} cannot be found");

            return Activator.CreateInstance(processorType) as IGroupedProcessor ??
                   throw new ConfigurationException($"The processor type {processorConfiguration.Type} does not implement IGroupedProcessor");
        }

        private IProcessor GetProcessor(Processor processorConfiguration)
        {
            processorConfiguration.ThrowIfNull(nameof(processorConfiguration));

            var processorType = Type.GetType(processorConfiguration.Type) ??
                                throw new ConfigurationException($"The processor type {processorConfiguration.Type} cannot be found");

            return Activator.CreateInstance(processorType) as IProcessor ??
                   throw new ConfigurationException($"The processor type {processorConfiguration.Type} does not implement IProcessor");
        }

        private IRule GetRule(Rule ruleConfiguration)
        {
            ruleConfiguration.ThrowIfNull(nameof(ruleConfiguration));

            var ruleType = Type.GetType(ruleConfiguration.Type) ??
                           throw new ConfigurationException($"The rule type {ruleConfiguration.Type} cannot be found");

            return Activator.CreateInstance(ruleType) as IRule ??
                   throw new ConfigurationException($"The rule type {ruleConfiguration.Type} does not implement IRule");
        }
    }
}