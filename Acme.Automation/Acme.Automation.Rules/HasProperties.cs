// <copyright file="HasProperties.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Acme.Automation.Core;

    /// <summary>
    /// Rule that check if properties is present.
    /// </summary>
    public class HasProperties : BaseRule<List<string>>
    {
        /// <inheritdoc />
        protected override bool IsMatch(List<string> properties, Message message)
        {
            return properties.All(x => message.Items.ContainsKey(x));
        }
    }
}