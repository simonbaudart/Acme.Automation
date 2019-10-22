// <copyright file="Action.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent a combination of rule and processor, if rule matches, the processor will be executed.
    /// </summary>
    [DataContract]
    public class Action
    {
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