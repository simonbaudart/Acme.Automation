// <copyright file="IActivator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Interface for an actuator.
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Gets or sets the id of the activator.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Start the actuator.
        /// </summary>
        /// <param name="config">The config, or null if no configuration.</param>
        void Start(JToken config);

        /// <summary>
        /// Wake the actuator if required.
        /// </summary>
        /// <param name="config">The config, or null if no configuration.</param>
        void WakeUp(JToken config);
    }
}