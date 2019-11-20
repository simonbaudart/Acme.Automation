// <copyright file="DumpDataConfiguration.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Configuration for dumping the data.
    /// </summary>
    [DataContract]
    public class DumpDataConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether the string must be truncated or not.
        /// </summary>
        /// <value>The Truncate.</value>
        [DataMember(Name = "truncate")]
        public bool Truncate { get; set; }
    }
}