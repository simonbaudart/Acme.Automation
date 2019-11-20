// <copyright file="BaseAction.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Acme.Automation.Core.Configuration;

    using log4net;

    using Action = Acme.Automation.Core.Configuration.Action;

    /// <summary>
    /// This represent a base action for all actions.
    /// </summary>
    [DataContract]
    public abstract class BaseAction : BaseLoger
    {
        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Run the specified action.
        /// </summary>
        /// <param name="configuration">The configuration of the automation.</param>
        /// <param name="job">The source job.</param>
        /// <param name="message">The message to process.</param>
        public void Run(AutomationConfiguration configuration, Job job, Message message)
        {
            this.Log.Info($"{job.Id} : Running the action type {this.Type}");
            this.InternalRun(configuration, job, message);
        }

        /// <summary>
        /// Run the specified action.
        /// </summary>
        /// <param name="configuration">The configuration of the automation.</param>
        /// <param name="job">The source job.</param>
        /// <param name="message">The message to process.</param>
        protected abstract void InternalRun(AutomationConfiguration configuration, Job job, Message message);
    }
}