// <copyright file="CurveReceipt.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Rules
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    /// <summary>
    /// Rule to check if message is from curve.
    /// </summary>
    public class CurveReceipt : BaseRule<EmptyConfiguration>
    {
        /// <inheritdoc />
        protected override bool IsMatch(EmptyConfiguration config, Message message)
        {
            var title = message.Items["subject"].ToString();

            return title.Contains("Curve Receipt:");
        }
    }
}