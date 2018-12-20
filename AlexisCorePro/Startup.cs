using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using AlexisCorePro.Infrastructure.Extensions;
using AlexisCorePro.Infrastructure.Filters;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Localization.Resources;

namespace AlexisCorePro
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public static IConfiguration Configuration { get; set; }

        public static IHostingEnvironment HostingEnvironment { get; set; }

        public static IStringLocalizer<SharedResource> StringLocalizer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(GlobalExceptionFilter));
                opt.Filters.Add(typeof(LanguageFilter));
                opt.Filters.Add(typeof(AuthenticationFilter));
            })
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                cfg.ImplicitlyValidateChildProperties = true;
            });

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AlexisPro"), optionsAction => optionsAction.EnableRetryOnFailure()));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.RegisterServices();
            services.RegisterJwt();
            services.AddAutoMapper();
            services.AddLocalization();

            services.AddSwaggerDocument(settings =>
            {
                settings.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                settings.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token",
                    new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = $"Copy 'Bearer ' + valid JWT token into field",
                        In = SwaggerSecurityApiKeyLocation.Header,
                    }));
            });

            // var serviceProvider = services.BuildServiceProvider(); In case we need to access service here
            // StringLocalizer = serviceProvider.GetService<IStringLocalizer<SharedResource>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseMvc();

            await app.MigrateDatabase();

            // Inject IServiceProvider serviceProvider
            // var service = serviceProvider.GetService<MyService>();
        }
    }
}
