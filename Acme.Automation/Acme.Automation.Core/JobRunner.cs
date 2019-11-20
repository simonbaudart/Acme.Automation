// <copyright file="JobRunner.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;
    using Acme.Core.Extensions;

    /// <summary>
    /// Main class for the worker.
    /// </summary>
    public class JobRunner : BaseLoger
    {
        /// <summary>
        /// Run the specified job.
        /// </summary>
        /// <param name="configuration">The complete configuration.</param>
        /// <param name="job">The job to be executed.</param>
        public void Run(AutomationConfiguration configuration, Job job)
        {
            this.Log.Info($"Starting the job : {job.FriendlyName}");

            var connectorConfiguration = configuration.Connectors.SingleOrDefault(x => x.Id == job.Connector) ??
                                         throw new ConfigurationException($"The connector {job.Connector} cannot be found");

            var connector = Factory.CreateConnector(connectorConfiguration);

            var messageProvider = connector as BaseMessageProvider ??
                                  throw new ConfigurationException($"The connector {job.Connector} is not a BaseMessageProvider");

            messageProvider.MessageReceived += (sender, message) => { this.ProcessMessageReceived(configuration, job, message); };

            connector.Execute(connectorConfiguration.Config);
        }

        private void ProcessMessageReceived(AutomationConfiguration configuration, Job job, Message message)
        {
            configuration.ThrowIfNull(nameof(configuration));
            job.ThrowIfNull(nameof(job));
            message.ThrowIfNull(nameof(message));

            this.Log.Info($"{job.Id} : A new message has been received");

            foreach (var actionRunner in job.Actions.Select(Factory.CreateAction))
            {
                actionRunner.Run(configuration, job, message);
            }
        }
    }
}