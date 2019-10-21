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

            var connectorConfiguration = configuration.Connectors?.SingleOrDefault(x => x.Id == job.Connector);

            if (connectorConfiguration == null)
            {
                throw new ConfigurationException($"The connector {job.Connector} cannot be found");
            }

            var connector = this.GetConnector(connectorConfiguration);
            var messages = connector.Execute(connectorConfiguration.Config);

            Log.Info($"{messages.Count} retrieved from the connector");

            Log.Info($"Done with the job : {job.FriendlyName}");
        }

        /// <summary>
        /// Get a connector with reflection.
        /// </summary>
        /// <param name="connectorConfiguration">The connector to be found.</param>
        /// <returns>The connector.</returns>
        private IConnector GetConnector(Connector connectorConfiguration)
        {
            connectorConfiguration.ThrowIfNull(nameof(connectorConfiguration));

            var connectorType = Type.GetType(connectorConfiguration.Type);

            if (connectorType == null)
            {
                throw new ConfigurationException($"The connector type {connectorConfiguration.Type} cannot be found");
            }

            var connector = Activator.CreateInstance(connectorType) as IConnector;

            if (connector == null)
            {
                throw new ConfigurationException($"The connector type {connectorConfiguration.Type} does not implement IConnector");
            }

            return connector;
        }
    }
}