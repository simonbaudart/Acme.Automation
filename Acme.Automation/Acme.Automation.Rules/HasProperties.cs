// <copyright file="HasProperties.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Rule that check if properties is present.
    /// </summary>
    public class HasProperties : IRule
    {
        /// <inheritdoc />
        public bool IsMatch(JToken config, Message message)
        {
            var properties = config.ToObject<List<string>>();
            return properties.All(x => message.Items.ContainsKey(x));
        }
    }
}