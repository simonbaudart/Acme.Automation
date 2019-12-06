// <copyright file="PaypalEmail.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Processors
{
    using System;
    using System.Linq;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    /// <summary>
    /// Convert a paypal email to receipt.
    /// </summary>
    public class PaypalEmail : BaseProcessor<EmptyConfiguration>
    {
        /// <inheritdoc />
        protected override void Execute(EmptyConfiguration configuration, Message message)
        {
        }
    }
}