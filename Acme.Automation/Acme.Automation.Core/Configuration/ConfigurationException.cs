// <copyright file="ConfigurationException.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;

    /// <summary>
    /// Exception that can occur when configuration is invalid.
    /// </summary>
    public class ConfigurationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message for the Exception.</param>
        public ConfigurationException(string message)
            : base(message)
        {
        }
    }
}