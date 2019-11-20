// <copyright file="IConnector.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// All connectors must implements this interface.
    /// </summary>
    public interface IConnector
    {
        /// <summary>
        /// Execute the connector.
        /// </summary>
        /// <param name="config">The config, or null if no configuration.</param>
        void Execute(JToken config);
    }
}