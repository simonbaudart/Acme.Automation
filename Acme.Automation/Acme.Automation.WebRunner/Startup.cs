// <copyright file="Startup.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.WebRunner
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    using Acme.Automation.Core.Configuration;

    using Hangfire;
    using Hangfire.SqlServer;

    using log4net;
    using log4net.Config;
    using log4net.Repository.Hierarchy;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        /// <summary>
        /// Define the logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Startup));

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard();
            this.StartAutomation();

            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // start log4net
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(this.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                })
                .UseLog4NetLogProvider());

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        private void StartAutomation()
        {
            var configurationPath = Path.Combine(this.Environment.ContentRootPath, "configuration.json");

            if (!File.Exists(configurationPath))
            {
                Log.Error($"Cannot find the configuration file {configurationPath} to register jobs.");
                return;
            }

            var configuration = AutomationConfiguration.Read(configurationPath);

            foreach (var job in configuration.Jobs.Where(job => job.RunAtStartup))
            {
                BackgroundJob.Enqueue<AutomationRunner>(automation => automation.Execute(configuration, job));
            }

            foreach (var job in configuration.Jobs.Where(job => !string.IsNullOrWhiteSpace(job.CronSchedule)))
            {
                RecurringJob.AddOrUpdate<AutomationRunner>(job.Id, automation => automation.Execute(configuration, job), job.CronSchedule, TimeZoneInfo.Utc);
            }
        }
    }
}