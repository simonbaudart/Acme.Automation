// <copyright file="AutomationRunner.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.WebRunner
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    public class AutomationRunner
    {
        public void RunAtStartup(AutomationConfiguration configuration, Job job)
        {
            new JobRunner().RunAtStartup(configuration, job);
        }

        public void RunRecurring(AutomationConfiguration configuration, Job job)
        {
            new JobRunner().RunRecurring(configuration, job);
        }
    }
}