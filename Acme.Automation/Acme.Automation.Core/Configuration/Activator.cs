// <copyright file="Activator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent an activator that can be started from a job.
    /// </summary>
    [DataContract]
    public class Activator : BaseTypedConfigurationItem
    {
    }
}