// <copyright file="AutomationRunner.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.WebRunner
{
    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    public class AutomationRunner
    {
        public void Execute(AutomationConfiguration configuration, Job job)
        {
            new JobRunner().Run(configuration, job);
        }
    }
}