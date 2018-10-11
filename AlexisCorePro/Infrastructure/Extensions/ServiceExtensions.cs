using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Business.Ships.Commands;
using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<DatabaseInitializer>();
            services.AddScoped<DatabaseSeed>();
            services.AddScoped<ShipService>();
            services.AddScoped<EnumsService>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<ShipCommandValidator>();

            return services;
        }

        public static IServiceCollection RegisterJwt(
            this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Startup.Configuration["JwtIssuer"],
                        ValidAudience = Startup.Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            return services;
        }
    }
}
