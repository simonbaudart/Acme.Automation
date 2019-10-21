//  <copyright file="IRule.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    public interface IRule
    {
        /// <summary>
        /// Determines if a rule match a message.
        /// </summary>
        /// <param name="config">The rule specific part of the config.</param>
        /// <param name="message">The message to be checked.</param>
        /// <returns>True if the rule succeed.</returns>
        bool IsMatch(JToken config, Message message);
    }
}