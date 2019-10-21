//  <copyright file="Rule.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.Core.Configuration
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a rule that can must be checked.
    /// </summary>
    [DataContract]
    public class Rule : BaseConfigurationItem
    {
    }
}