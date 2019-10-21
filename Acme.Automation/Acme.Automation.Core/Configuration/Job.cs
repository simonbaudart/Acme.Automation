//  <copyright file="Job.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a job.
    /// </summary>
    [DataContract]
    public class Job : BaseConfigurationItem
    {
        /// <summary>
        /// Gets or sets the RunAtStartup.
        /// If true, when the host start, it should run this job.
        /// </summary>
        /// <value>The RunAtStartup.</value>
        [DataMember(Name = "runAtStartup")]
        public bool RunAtStartup { get; set; }

        /// <summary>
        /// Gets or sets the CronSchedule.
        /// If not null, the host must schedule this job based on this cron.
        /// </summary>
        /// <value>The CronSchedule.</value>
        [DataMember(Name = "cronSchedule")]
        public string CronSchedule { get; set; }

        /// <summary>
        /// Gets or sets the Connector.
        /// </summary>
        /// <value>The Connector.</value>
        [DataMember(Name = "connector")]
        public string Connector { get; set; }

        /// <summary>
        /// Gets or sets the Rule.
        /// </summary>
        /// <value>The Rule.</value>
        [DataMember(Name = "rule")]
        public string Rule { get; set; }

        /// <summary>
        /// Gets or sets the Processor.
        /// </summary>
        /// <value>The Processor.</value>
        [DataMember(Name = "processor")]
        public string Processor { get; set; }
    }
}