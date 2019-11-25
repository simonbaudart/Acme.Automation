// <copyright file="PingActivator.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Activators
{
    using System;
    using System.Linq;
    using System.Threading;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    /// <summary>
    /// A very simple actuator : ping a new message every minute from a thread.
    /// </summary>
    public class PingActivator : BaseActivator<EmptyConfiguration>
    {
        private Thread pingThread;

        protected override void Start(EmptyConfiguration config)
        {
            this.pingThread = new Thread(() =>
            {
                this.Log.Info("Ping");
                var message = new Message();
                message.Add("ping", "pong");
                this.RaiseMessageReceived(message);

                Thread.Sleep(60000);
            });

            this.pingThread.Start();
        }

        protected override void WakeUp(EmptyConfiguration config)
        {
            this.Log.Info("I'm awake !");
        }
    }
}