//  <copyright file="ConfigurationException.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;

    public class ConfigurationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" />.
        /// </summary>
        /// <param name="message"></param>
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}