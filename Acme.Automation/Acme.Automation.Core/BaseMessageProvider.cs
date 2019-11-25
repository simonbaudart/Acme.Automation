// <copyright file="BaseMessageProvider.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core
{
    using System;
    using System.Linq;
    using System.Threading;

    using Acme.Core.Extensions;

    /// <summary>
    /// Base classe for all message providers : connectors and activators.
    /// </summary>
    public abstract class BaseMessageProvider : BaseLoger
    {
        /// <summary>
        /// Handle a message received.
        /// </summary>
        /// <param name="sender">The sender that gets the message.</param>
        /// <param name="message">The message that has been received.</param>
        public delegate void MessageReceivedHandler(object sender, Message message);

        /// <summary>
        /// Event raised when a message is received from the provider.
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// Raise the event "MessageReceived" with the specific message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void RaiseMessageReceived(Message message)
        {
            message.ThrowIfNull(nameof(message));

            var thread = new Thread(() => { this.MessageReceived?.Invoke(this, message); });
            thread.Start();
        }
    }
}