// <copyright file="JobRunner.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core.Configuration;
    using Acme.Core.Extensions;

    /// <summary>
    /// Main class for the worker.
    /// </summary>
    public class JobRunner : BaseLoger
    {
        private static readonly List<IActivator> RunningActivators = new List<IActivator>();

        /// <summary>
        /// Run the specified job at startup.
        /// </summary>
        /// <param name="configuration">The complete configuration.</param>
        /// <param name="job">The job to be executed.</param>
        public void RunAtStartup(AutomationConfiguration configuration, Job job)
        {
            this.Log.Info($"Starting the job : {job.FriendlyName}");

            this.RunConnector(configuration, job);
            this.StartActivator(configuration, job);
        }

        /// <summary>
        /// Run the specified job multiple times.
        /// </summary>
        /// <param name="configuration">The complete configuration.</param>
        /// <param name="job">The job to be executed.</param>
        public void RunRecurring(AutomationConfiguration configuration, Job job)
        {
            this.Log.Info($"Running the job : {job.FriendlyName}");
            this.RunConnector(configuration, job);
            this.WakeUpActivator(configuration, job);
        }

        private void ProcessMessageReceived(AutomationConfiguration configuration, Job job, Message message)
        {
            configuration.ThrowIfNull(nameof(configuration));
            job.ThrowIfNull(nameof(job));
            message.ThrowIfNull(nameof(message));

            this.Log.Info($"{job.Id} : A new message has been received");

            foreach (var actionRunner in job.Actions.Select(Factory.CreateAction))
            {
                try
                {
                    actionRunner.Run(configuration, job, message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void RunConnector(AutomationConfiguration configuration, Job job)
        {
            if (string.IsNullOrEmpty(job.Connector))
            {
                return;
            }

            this.Log.Info($"Starting the connector {job.Connector}");

            var connectorConfiguration = configuration.Connectors.SingleOrDefault(x => x.Id == job.Connector) ??
                                         throw new ConfigurationException($"The connector {job.Connector} cannot be found");

            var connector = Factory.CreateConnector(connectorConfiguration);

            var messageProvider = connector as BaseMessageProvider ??
                                  throw new ConfigurationException($"The connector {job.Connector} is not a BaseMessageProvider");

            messageProvider.MessageReceived += (sender, message) => { this.ProcessMessageReceived(configuration, job, message); };

            connector.Execute(connectorConfiguration.Config);
        }

        private void StartActivator(AutomationConfiguration configuration, Job job)
        {
            if (string.IsNullOrEmpty(job.Activator))
            {
                return;
            }

            this.Log.Info($"Starting the activator {job.Activator}");

            var activatorConfiguration = configuration.Activators.SingleOrDefault(x => x.Id == job.Activator) ??
                                         throw new ConfigurationException($"The activator {job.Activator} cannot be found");

            var activator = Factory.CreateActivator(activatorConfiguration);

            var messageProvider = activator as BaseMessageProvider ??
                                  throw new ConfigurationException($"The activator {job.Activator} is not a BaseMessageProvider");

            messageProvider.MessageReceived += (sender, message) => { this.ProcessMessageReceived(configuration, job, message); };

            lock (RunningActivators)
            {
                RunningActivators.Add(activator);
                activator.Start(activatorConfiguration.Config);
            }
        }

        private void WakeUpActivator(AutomationConfiguration configuration, Job job)
        {
            if (string.IsNullOrEmpty(job.Activator))
            {
                return;
            }

            this.Log.Info($"WakeUp the activator {job.Activator}");

            var activatorConfiguration = configuration.Activators.SingleOrDefault(x => x.Id == job.Activator) ??
                                         throw new ConfigurationException($"The activator {job.Activator} cannot be found");

            IActivator runningActivator;

            lock (RunningActivators)
            {
                runningActivator = RunningActivators.FirstOrDefault(x => x.Id == job.Activator);

                if (runningActivator == null)
                {
                    throw new ConfigurationException($"The activator {job.Activator} is not running, ensure the RunAtStartup is true.");
                }
            }

            runningActivator.WakeUp(activatorConfiguration.Config);
        }
    }
}