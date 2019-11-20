// <copyright file="BaseLoger.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using log4net;

    /// <summary>
    /// Base class for everything that must log.
    /// </summary>
    public abstract class BaseLoger
    {
        private ILog log;

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILog Log => this.log ?? (this.log = LogManager.GetLogger(this.GetType()));
    }
}