// <copyright file="AlwaysMatch.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This rule ... always match ! Yeah, really !.
    /// </summary>
    public class AlwaysMatch : IRule
    {
        /// <inheritdoc />
        public bool IsMatch(JToken config, Message message)
        {
            return true;
        }
    }
}