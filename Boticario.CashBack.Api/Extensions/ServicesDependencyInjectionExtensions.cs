// ----------------------------------------
// <copyright file=ServicesDependencyInjectionExtensions.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.CashBack.Api.Extensions
{
    /// <summary>
    /// Add dependencies
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ServicesDependencyInjectionExtensions 
    {
        #region [ Public methods ]


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IExternalService, ExternalService>();
            services.AddScoped<IHttpClientHelper, HttpClientHelper>();
            return services;
        } 

        #endregion [ Public methods ]
    }
}