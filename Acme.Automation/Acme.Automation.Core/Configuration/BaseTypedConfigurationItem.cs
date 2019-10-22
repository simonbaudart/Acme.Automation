// <copyright file="BaseTypedConfigurationItem.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// When a configuration has a type and a sub config, it must inherit from this.
    /// </summary>
    [DataContract]
    public abstract class BaseTypedConfigurationItem : BaseConfigurationItem
    {
        /// <summary>
        /// Gets or sets the Config.
        /// </summary>
        /// <value>The Config.</value>
        [DataMember(Name = "config")]
        public JToken Config { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}