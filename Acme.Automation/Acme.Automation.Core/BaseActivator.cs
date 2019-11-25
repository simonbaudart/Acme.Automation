// <copyright file="BaseActivator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A base implementation of <see cref="IActivator" />.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    /// <seealso cref="IActivator" />
    public abstract class BaseActivator<TConfiguration> : BaseMessageProvider, IActivator
        where TConfiguration : class
    {
        /// <inheritdoc />
        void IActivator.Start(JToken config)
        {
            this.Start(config?.ToObject<TConfiguration>());
        }

        /// <inheritdoc />
        void IActivator.WakeUp(JToken config)
        {
            this.Start(config?.ToObject<TConfiguration>());
        }

        /// <summary>
        /// Start the actuator.
        /// </summary>
        /// <param name="config">The config, or null if no configuration.</param>
        protected abstract void Start(TConfiguration config);

        /// <summary>
        /// Wake the actuator if required.
        /// </summary>
        /// <param name="config">The config, or null if no configuration.</param>
        protected abstract void WakeUp(TConfiguration config);
    }
}