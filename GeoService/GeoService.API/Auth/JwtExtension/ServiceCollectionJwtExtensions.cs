using GeoService.DAL.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using static GeoService.API.Auth.Identity.Contracts;

namespace GeoService.API.Auth.JwtExtension
{
    public static class ServiceCollectionJwtExtensions
    {
        public static IServiceCollection AddApiJwtAuthentication(this IServiceCollection services, JwtOptions tokenOptions)
        {
            if (tokenOptions == null)
                throw new ArgumentNullException(
                    $"{nameof(tokenOptions)} обязательный параметр. Пожалуйста, убедитесь, что вы предоставили действительный экземпляр с соответствующей конфигурацией.");

            services.AddScoped(serviceProvider => new JwtTokenGenerator(tokenOptions));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateAudience = false,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuer = false,
                        ValidIssuer = tokenOptions.Issuer,
                        IssuerSigningKey = tokenOptions.SigningKey,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdminPolicy, policy => policy.RequireRole(nameof(RoleEnum.Admin), nameof(RoleEnum.Leader), nameof(RoleEnum.Participant), nameof(RoleEnum.NonDefined)));
                options.AddPolicy(LeaderPolicy, policy => policy.RequireRole(nameof(RoleEnum.Leader), nameof(RoleEnum.Participant)));
                options.AddPolicy(ParticipantPolicy, policy => policy.RequireRole(nameof(RoleEnum.Participant)));
                options.AddPolicy(NonDefinedPolicy, policy => policy.RequireRole(nameof(RoleEnum.NonDefined)));
            });

            return services;
        }
    }
}
