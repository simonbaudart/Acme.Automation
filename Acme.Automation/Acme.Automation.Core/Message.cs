//  <copyright file="Message.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

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
        /// Initializes a new instance of the <see cref="Message" />
        /// </summary>
        public Message()
        {
            this.Items = new Dictionary<string, JToken>();
        }

        /// <summary>
        /// Gets or sets the Items.
        /// </summary>
        /// <value>The Items.</value>
        public Dictionary<string, JToken> Items { get; set; }
    }
}