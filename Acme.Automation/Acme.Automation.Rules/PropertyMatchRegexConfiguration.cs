// <copyright file="PropertyMatchRegexConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for the property to match a regex.
    /// </summary>
    [DataContract]
    public class PropertyMatchRegexConfiguration
    {
        /// <summary>
        /// Gets or sets the Match.
        /// </summary>
        /// <value>The Match.</value>
        [DataMember(Name = "match")]
        public string Match { get; set; }

        /// <summary>
        /// Gets or sets the Property.
        /// </summary>
        /// <value>The Property.</value>
        [DataMember(Name = "property")]
        public string Property { get; set; }
    }
}