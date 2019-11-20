// <copyright file="SimpleAction.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Actions
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;

    /// <summary>
    /// Simple action.
    /// </summary>
    public class SimpleAction : BaseAction
    {
        /// <inheritdoc />
        protected override void InternalRun(AutomationConfiguration configuration, Job job, Message message)
        {
            var processor = Factory.CreateProcessor(configuration, this.ActionConfiguration.Processor);

            if (string.IsNullOrEmpty(this.ActionConfiguration.RuleId))
            {
                processor.Execute(job, message);
                return;
            }

            var rule = Factory.CreateRule(configuration, this.ActionConfiguration.RuleId);

            if (rule.IsMatch(job, message))
            {
                processor.Execute(job, message);
            }
        }
    }
}