// ----------------------------------------
// <copyright file=Program.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Core;
using Boticario.CashBack.Repositories.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Boticario.CashBack.Api
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        #region [ Static fields ]

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion [ Static fields ]

        #region [ Public static methods ]

        public static void Main(string[] args)
        {
            try
            {
                CreatLogDb();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Can not create Log database.");
            }

            try
            {
                logger.Info($"{DateTime.Now} - Initialize service app.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{DateTime.Now} - Error:{ex.Message}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddConsole();
                })
            .UseNLog();

        public static void CreatLogDb()
        {
            var options = new DbContextOptionsBuilder<LogContext>()
                .UseSqlite(AppConfig.LogDb)
                .Options;

            var context = new LogContext(options);
            context.CreateDataBase();
        } 

        #endregion [ Public static methods ]
    }

}
