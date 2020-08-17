// ----------------------------------------
// <copyright file=Startup.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api.Extensions;
using Boticario.CashBack.Core;
using Boticario.CashBack.Core.Extensions;
using Boticario.CashBack.Repositories.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Boticario.CashBack.Api
{
    /// <summary>
    /// Start App
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string UriString = @"https://www.linkedin.com/in/wagner-andre-eduardo-b1252a49/";

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion [ Constructors ]

        #region [ Public methods ]

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Boticario API",
                    Contact = new OpenApiContact
                    {
                        Name = "Wagner",
                        Email = "wageduardo@hotmail.com",
                        Url = new Uri(UriString)
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
               "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            SqliteConnection resConn = GetBoticarioConnection();
            services.AddDbContext<BoticarioContext>(opt => opt.UseSqlite(resConn));

            services.AddDbContext<LogContext>(opt => opt.UseSqlite(AppConfig.LogDb));
            services.AddTokenConfiguration();
            services.AddRepositories();
            services.AddServices();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              BoticarioContext boticarioContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials()
            );

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = "swagger/ui";
            });

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            boticarioContext.Database.EnsureCreated();
            boticarioContext.InsertDataTest();
        } 

        #endregion [ Public methods ]

        #region [ Private methods ]

        /// <summary>
        /// Gets the boticario connection.
        /// </summary>
        /// <returns></returns>
        private SqliteConnection GetBoticarioConnection()
        {
            var connection = AppConfig.SqliteConnectionString;
            var conn = new SqliteConnection(connection);
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = "PRAGMA journal_mode=WAL;";
            command.ExecuteNonQuery();
            return conn;
        } 

        #endregion [ Private methods ]
    }
}
