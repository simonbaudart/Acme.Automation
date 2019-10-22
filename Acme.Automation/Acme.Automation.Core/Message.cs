// <copyright file="Message.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This represents a message that is get from a connector, checked by rules and executed by processor.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The Items.
        /// </value>
        public Dictionary<string, JToken> Items { get; } = new Dictionary<string, JToken>();
    }
}