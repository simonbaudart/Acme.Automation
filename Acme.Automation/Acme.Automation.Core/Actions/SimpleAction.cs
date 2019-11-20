// <copyright file="SimpleAction.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Actions
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Acme.Automation.Core.Configuration;

    /// <summary>
    /// Simple action.
    /// </summary>
    [DataContract]
    public class SimpleAction : BaseAction
    {
        /// <summary>
        /// Gets or sets the Processor.
        /// </summary>
        /// <value>The Processor.</value>
        [DataMember(Name = "processor")]
        public string Processor { get; set; }

        /// <summary>
        /// Gets or sets the RuleId.
        /// </summary>
        /// <value>The RuleId.</value>
        [DataMember(Name = "ruleId")]
        public string RuleId { get; set; }

        /// <inheritdoc />
        protected override Message InternalRun(AutomationConfiguration configuration, Job job, Message message)
        {
            var processor = Factory.CreateProcessor(configuration, this.Processor);

            if (string.IsNullOrEmpty(this.RuleId))
            {
                processor.Execute(job, message);
                return message;
            }

            var rule = Factory.CreateRule(configuration, this.RuleId);

            if (rule.IsMatch(job, message))
            {
                processor.Execute(job, message);
            }

            return message;
        }
    }
}