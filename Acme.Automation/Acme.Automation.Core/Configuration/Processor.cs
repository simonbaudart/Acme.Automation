// <copyright file="Processor.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a connector that can be executed from a job.
    /// </summary>
    [DataContract]
    public class Processor : BaseTypedConfigurationItem
    {
    }
}