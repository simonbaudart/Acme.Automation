// <copyright file="BaseConnector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A base implementation of <see cref="IConnector" />.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    /// <seealso cref="Acme.Automation.Core.IConnector" />
    /// <seealso cref="IConnector" />
    public abstract class BaseConnector<TConfiguration> : IConnector
    {
        /// <inheritdoc />
        List<Message> IConnector.Execute(JToken config)
            => this.Execute(config.ToObject<TConfiguration>());

        /// <summary>
        /// Execute the connector.
        /// </summary>
        /// <param name="configuration">The configuration, or null if no configuration.</param>
        /// <returns>
        /// List of messages.
        /// </returns>
        public abstract List<Message> Execute(TConfiguration configuration);
    }
}