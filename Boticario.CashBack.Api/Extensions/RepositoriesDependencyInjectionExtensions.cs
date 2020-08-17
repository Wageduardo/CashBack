// ----------------------------------------
// <copyright file=RepositoriesDependencyInjectionExtensions.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Boticario.CashBack.Api.Extensions
{
    /// <summary>
    /// Add dependencies
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class RepositoriesDependencyInjectionExtensions
    {
        #region [ Public methods ]

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            return services;
        } 

        #endregion [ Public methods ]
    }
}
