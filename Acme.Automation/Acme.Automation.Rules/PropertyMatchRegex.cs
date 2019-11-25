// <copyright file="PropertyMatchRegex.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Acme.Automation.Core;

    /// <summary>
    /// Try to match a regex on a property.
    /// </summary>
    public class PropertyMatchRegex : BaseRule<PropertyMatchRegexConfiguration>
    {
        /// <inheritdoc />
        protected override bool IsMatch(PropertyMatchRegexConfiguration configuration, Message message)
        {
            if (!message.Items.ContainsKey(configuration.Property))
            {
                return false;
            }

            var regex = new Regex(configuration.Match);
            return regex.IsMatch(message.Items[configuration.Property].ToString());
        }
    }
}