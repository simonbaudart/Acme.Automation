// <copyright file="IRule.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Acme.Automation.Core.Configuration;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contract for a rule.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Gets or sets the rule configuration.
        /// </summary>
        Rule RuleConfiguration { get; set; }

        /// <summary>
        /// Determines if a rule match a message.
        /// </summary>
        /// <param name="job">The source job.</param>
        /// <param name="message">The message to be checked.</param>
        /// <returns>True if the rule matches.</returns>
        bool IsMatch(Job job, Message message);
    }
}