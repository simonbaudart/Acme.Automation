// <copyright file="Job.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Represents a job.
    /// </summary>
    [DataContract]
    public class Job : BaseConfigurationItem
    {
        /// <summary>
        /// Gets the actions.
        /// </summary>
        [DataMember(Name = "actions")]
        public List<JToken> Actions { get; } = new List<JToken>();

        /// <summary>
        /// Gets or sets the Actuator.
        /// </summary>
        /// <value>The Actuator.</value>
        [DataMember(Name = "activator")]
        public string Activator { get; set; }

        /// <summary>
        /// Gets or sets the Connector.
        /// </summary>
        /// <value>The Connector.</value>
        [DataMember(Name = "connector")]
        public string Connector { get; set; }

        /// <summary>
        /// Gets or sets the CronSchedule.
        /// If not null, the host must schedule this job based on this cron.
        /// </summary>
        /// <value>The CronSchedule.</value>
        [DataMember(Name = "cronSchedule")]
        public string CronSchedule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the job runs at startup.
        /// If true, when the host start, it should run this job.
        /// </summary>
        /// <value>The RunAtStartup.</value>
        [DataMember(Name = "runAtStartup")]
        public bool RunAtStartup { get; set; }
    }
}