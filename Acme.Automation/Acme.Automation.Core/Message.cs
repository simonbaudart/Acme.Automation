// <copyright file="Message.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        /// <summary>
        /// Gets an items converted into a specific type.
        /// </summary>
        /// <param name="key">The key of item to be found.</param>
        /// <typeparam name="T">The type that must bu returned.</typeparam>
        /// <returns>The value of the item, converted to type T.</returns>
        public T Get<T>(string key)
        {
            return this.Items.ContainsKey(key) ? this.Items[key].ToObject<T>() : default;
        }
    }
}