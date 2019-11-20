// <copyright file="Factory.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Actions;
    using Acme.Automation.Core.Configuration;
    using Acme.Core.Extensions;

    using Newtonsoft.Json.Linq;

    using Action = Acme.Automation.Core.Configuration.Action;

    /// <summary>
    /// Factory for all core related classes.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Create an action based on the configuration.
        /// </summary>
        /// <param name="actionConfiguration">The configuration for the action.</param>
        /// <returns>The action in its base type.</returns>
        public static BaseAction CreateAction(JToken actionConfiguration)
        {
            actionConfiguration.ThrowIfNull(nameof(actionConfiguration));

            var actionType = actionConfiguration["type"].ToString();
            switch (actionType)
            {
                case "simple":
                    return actionConfiguration.ToObject<SimpleAction>();
            }

            throw new ConfigurationException($"The action type {actionType} does not exists.");
        }

        /// <summary>
        /// Create a connector with reflection.
        /// </summary>
        /// <param name="connectorConfiguration">The connector to be found.</param>
        /// <returns>The connector.</returns>
        public static IConnector CreateConnector(Connector connectorConfiguration)
        {
            connectorConfiguration.ThrowIfNull(nameof(connectorConfiguration));

            var connectorType = Type.GetType(connectorConfiguration.Type) ??
                                throw new ConfigurationException($"The connector type {connectorConfiguration.Type} cannot be found");

            return Activator.CreateInstance(connectorType) as IConnector ??
                   throw new ConfigurationException($"The connector type {connectorConfiguration.Type} does not implement IConnector");
        }

        /// <summary>
        /// Create a new instance of the processor.
        /// </summary>
        /// <param name="automationConfiguration">The configuration file of the automation.</param>
        /// <param name="processorId">The id of the processor to create.</param>
        /// <returns>The instance of the processor.</returns>
        public static IProcessor CreateProcessor(AutomationConfiguration automationConfiguration, string processorId)
        {
            var processorConfiguration = automationConfiguration.Processors.SingleOrDefault(x => x.Id == processorId) ??
                                         throw new ConfigurationException($"The processor {processorId} cannot be found");

            return CreateProcessor(processorConfiguration);
        }

        /// <summary>
        /// Create a new instance of the processor.
        /// </summary>
        /// <param name="processorConfiguration">The processor configuration.</param>
        /// <returns>The instance of the processor.</returns>
        public static IProcessor CreateProcessor(Processor processorConfiguration)
        {
            processorConfiguration.ThrowIfNull(nameof(processorConfiguration));

            var processorType = Type.GetType(processorConfiguration.Type) ??
                                throw new ConfigurationException($"The processor type {processorConfiguration.Type} cannot be found");

            var instance = Activator.CreateInstance(processorType) as IProcessor ??
                           throw new ConfigurationException($"The processor type {processorConfiguration.Type} does not implement IProcessor");

            instance.ProcessorConfiguration = processorConfiguration;

            return instance;
        }

        /// <summary>
        /// Create a new instance of the processor.
        /// </summary>
        /// <param name="automationConfiguration">The configuration file of the automation.</param>
        /// <param name="ruleId">The id of the rule to create.</param>
        /// <returns>The instance of the rule.</returns>
        public static IRule CreateRule(AutomationConfiguration automationConfiguration, string ruleId)
        {
            var ruleConfiguration = automationConfiguration.Rules.SingleOrDefault(x => x.Id == ruleId) ??
                                    throw new ConfigurationException($"The rule {ruleId} cannot be found");

            return CreateRule(ruleConfiguration);
        }

        /// <summary>
        /// Create a ne instance of the rule.
        /// </summary>
        /// <param name="ruleConfiguration">The rule configuration.</param>
        /// <returns>The instance of the rule.</returns>
        public static IRule CreateRule(Rule ruleConfiguration)
        {
            ruleConfiguration.ThrowIfNull(nameof(ruleConfiguration));

            var ruleType = Type.GetType(ruleConfiguration.Type) ??
                           throw new ConfigurationException($"The rule type {ruleConfiguration.Type} cannot be found");

            var instance = Activator.CreateInstance(ruleType) as IRule ??
                           throw new ConfigurationException($"The rule type {ruleConfiguration.Type} does not implement IRule");

            instance.RuleConfiguration = ruleConfiguration;

            return instance;
        }
    }
}