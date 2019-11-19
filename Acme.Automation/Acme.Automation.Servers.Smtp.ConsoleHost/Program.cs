// <copyright file="Program.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Servers.Smtp.ConsoleHost
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Xml;

    using log4net;
    using log4net.Config;
    using log4net.Repository.Hierarchy;

    internal class Program
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private static async Task Main(string[] args)
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            Log.Info("START CONSOLE");

            var server = new SmtpServerListener("localhost", 25, 587);
            await server.Start();

            Console.ReadKey();

            Log.Info("STOP CONSOLE");
        }
    }
}