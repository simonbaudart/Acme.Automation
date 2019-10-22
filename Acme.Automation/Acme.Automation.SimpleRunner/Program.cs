// <copyright file="Program.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.SimpleRunner
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    using Acme.Automation.Core;
    using Acme.Automation.Core.Configuration;

    using log4net;
    using log4net.Config;
    using log4net.Repository.Hierarchy;

    /// <summary>
    /// Main entry point for SimpleRunner.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Start the program.
        /// </summary>
        /// <param name="args">Arguments of the console.</param>
        private static void Main(string[] args)
        {
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            Log.Info("START CONSOLE");

            var configuration = AutomationConfiguration.Read("configuration.json");
            foreach (var job in configuration.Jobs.Where(job => job.RunAtStartup))
            {
                new Worker().Execute(configuration, job);
            }

            Log.Info("STOP CONSOLE");
        }
    }
}