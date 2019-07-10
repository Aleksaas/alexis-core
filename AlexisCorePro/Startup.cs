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
using NSwag.AspNetCore;
using Localization.Resources;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Server;

namespace AlexisCorePro
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public static ILoggerFactory MyLoggerFactory { get; set; }

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

            services.AddLogging(builder => builder
                .AddDebug()
                .AddFilter(level => level >= LogLevel.Information)
            );

            MyLoggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();

            services.AddDbContext<DatabaseContext>(options =>
                options
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(Configuration.GetConnectionString("AlexisPro"), optionsAction => optionsAction.EnableRetryOnFailure()));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.RegisterServices();
            services.AddAutoMapper();
            services.AddLocalization();

            services.RegisterJwt();
            services.RegisterSwagger();

            services.AddScoped<IDependencyResolver>(x =>
                new FuncDependencyResolver(x.GetRequiredService));

            services.AddScoped<DatabaseGraphQLSchema>();

            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; // set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped);

            // var serviceProvider = services.BuildServiceProvider(); In case we need to access service here
            // StringLocalizer = serviceProvider.GetService<IStringLocalizer<SharedResource>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c => c.SwaggerRoutes.Add(new SwaggerUi3Route("Service API V1", "/swagger/v1/swagger.json")));

            app.UseCors("AllowAll");

            app.UseMvc();
            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.MigrateDatabase();

            app.UseGraphQL<DatabaseGraphQLSchema>("/graphql");
            // app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground
            // app.UseGraphiQl("/graphql");

            // Inject IServiceProvider serviceProvider
            // var service = serviceProvider.GetService<MyService>();
        }
    }
}
