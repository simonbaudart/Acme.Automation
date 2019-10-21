//  <copyright file="Connector.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent a connecter that can be run from a job.
    /// </summary>
    [DataContract]
    public class Connector : BaseConfigurationItem
    {
    }
}