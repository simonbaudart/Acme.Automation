// <copyright file="SequenceAction.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Acme.Automation.Core.Configuration;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Actions that must be ran in sequence for a specific message.
    /// </summary>
    [DataContract]
    public class SequenceAction : BaseAction
    {
        /// <summary>
        /// Gets or sets the Actions.
        /// </summary>
        /// <value>The Actions.</value>
        [DataMember(Name = "actions")]
        public List<JToken> Actions { get; set; }

        /// <inheritdoc />
        protected override Message InternalRun(AutomationConfiguration configuration, Job job, Message message)
        {
            return this.Actions.Select(Factory.CreateAction).Aggregate(message, (current, action) => action.Run(configuration, job, current));
        }
    }
}