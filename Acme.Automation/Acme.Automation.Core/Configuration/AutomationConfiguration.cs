// <copyright file="AutomationConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;

    using Acme.Core.Extensions;

    using Newtonsoft.Json;

    /// <summary>
    /// Represent the base classe for all the configuration.
    /// See the SampleConfiguration.json for the schema.
    /// </summary>
    [DataContract]
    public class AutomationConfiguration
    {
        /// <summary>
        /// Gets the Connectors.
        /// </summary>
        /// <value>The Connectors.</value>
        [DataMember(Name = "connectors")]
        public List<Connector> Connectors { get; } = new List<Connector>();

        /// <summary>
        /// Gets the jobs.
        /// Jobs are intended to run at startup and/or being scheduled.
        /// </summary>
        /// <value>The jobs.</value>
        [DataMember(Name = "jobs")]
        public List<Job> Jobs { get; } = new List<Job>();

        /// <summary>
        /// Gets the Processors.
        /// </summary>
        /// <value>The Processors.</value>
        [DataMember(Name = "processors")]
        public List<Processor> Processors { get; } = new List<Processor>();

        /// <summary>
        /// Gets the Rules.
        /// </summary>
        /// <value>The Rules.</value>
        [DataMember(Name = "rules")]
        public List<Rule> Rules { get; } = new List<Rule>();

        /// <summary>
        /// Read the configuration from a file.
        /// Also perform the ensure validity to be sure configuration is correct.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <returns>The configuration if correct.</returns>
        public static AutomationConfiguration Read(string path)
        {
            path.ThrowIfNull(nameof(path));

            if (!File.Exists(path))
            {
                throw new ConfigurationException("The specified file does not exists.");
            }

            var content = File.ReadAllText(path);
            var configuration = JsonConvert.DeserializeObject<AutomationConfiguration>(content);
            configuration.EnsureValidity();
            return configuration;
        }

        /// <summary>
        /// Ensure that the configuration is valid by performing some checks.
        /// </summary>
        public void EnsureValidity()
        {
            this.Jobs.ForEach(job =>
            {
                var connector = this.Connectors.SingleOrDefault(x => x.Id == job.Connector);

                if (connector == null)
                {
                    throw new ConfigurationException($"The connector {job.Connector} cannot be found");
                }

                // Actions are optionals
                job.Actions?.ForEach(action =>
                {
                    var rule = this.Rules.SingleOrDefault(x => x.Id == action.RuleId) ??
                               throw new ConfigurationException($"The rule {action.RuleId} cannot be found");

                    var processor = this.Processors.SingleOrDefault(x => x.Id == action.Processor);

                    if (processor == null)
                    {
                        throw new ConfigurationException($"The processor {action.Processor} cannot be found");
                    }
                });
            });
        }
    }
}