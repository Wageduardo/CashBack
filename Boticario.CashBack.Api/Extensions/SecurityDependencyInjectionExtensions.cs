// ----------------------------------------
// <copyright file=SecurityDependencyInjectionExtensions.cs company=Boticario>
// Copyright (c) [Boticario] [2020]. Confidential.  All Rights Reserved
// </copyright>
// ----------------------------------------
using Boticario.CashBack.Api.Extensions.SecurityRoles;
using Boticario.CashBack.Core.SecurityRoles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Boticario.CashBack.Core.Extensions
{
    /// <summary>
    /// JWT Config
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class JwtTokenExtension
    {
        #region [ Public methods ]

        public static IServiceCollection AddTokenConfiguration(this IServiceCollection services)
        {
            var key = AppConfig.Key;
            var symmetricKeyAsBase64 = key;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateIssuer = true,
                ValidIssuer = AppConfig.Issuer,

                ValidateAudience = true,
                ValidAudience = AppConfig.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,

                NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Configuration = new OpenIdConnectConfiguration { TokenEndpoint = "/token" };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.All, policy => policy.RequireRole(Roles.ReadOnly, Roles.OperationalUser, Roles.SolutionAdministrator));
                options.AddPolicy(Policies.User, policy => policy.RequireRole(Roles.OperationalUser, Roles.SolutionAdministrator));
                options.AddPolicy(Policies.Administrator, policy => policy.RequireRole(Roles.SolutionAdministrator));
            });
            return services;
        } 

        #endregion [ Public methods ]
    }
}
