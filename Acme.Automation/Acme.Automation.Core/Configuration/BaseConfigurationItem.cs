//  <copyright file="BaseConfigurationItem.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for all item in configuration that have and identifier and a friendly name.
    /// </summary>
    [DataContract]
    public abstract class BaseConfigurationItem
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The Id.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the FriendlyName.
        /// </summary>
        /// <value>The FriendlyName.</value>
        [DataMember(Name = "friendlyName")]
        public string FriendlyName { get; set; }
    }
}