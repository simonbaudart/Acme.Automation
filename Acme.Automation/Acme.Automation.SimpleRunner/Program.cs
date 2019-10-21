//  <copyright file="Program.cs" company="Acme">
//  Copyright (c) Acme. All rights reserved.
//  </copyright>

namespace Acme.Automation.SimpleRunner
{
    using System;
    using System.IO;
    using System.Linq;

    using Acme.Automation.Core.Configuration;

    using Newtonsoft.Json;

    internal class Program
    {
        /// <summary>
        /// Start the program.
        /// </summary>
        /// <param name="args">Arguments of the console.</param>
        private static void Main(string[] args)
        {
            var configuration = AutomationConfiguration.Read("C:\\TMP\\SBA\\Acme.Automation.json");

        }
    }
}