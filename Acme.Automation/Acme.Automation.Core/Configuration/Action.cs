// <copyright file="Action.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent a combination of rule and processor, if rule matches, the processor will be executed.
    /// </summary>
    [DataContract]
    public class Action
    {
        /// <summary>
        /// Gets or sets the Processor.
        /// </summary>
        /// <value>The Processor.</value>
        [DataMember(Name = "processor")]
        public string Processor { get; set; }

        /// <summary>
        /// Gets or sets the Rule id.
        /// </summary>
        /// <value>The Rule.</value>
        [DataMember(Name = "ruleId")]
        public string RuleId { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}