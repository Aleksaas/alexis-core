using AlexisCorePro.Domain;
using AlexisCorePro.Infrastructure.Extensions;
using AlexisCorePro.Infrastructure.Filters;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
            })
            .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); })
            .AddFluentValidation(cfg => {
                cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                cfg.ImplicitlyValidateChildProperties = true;
            });

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            if (HostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("AlexisProInMemoryDb"));
            }
            else
            {
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AlexisPro"), optionsAction => optionsAction.EnableRetryOnFailure()));
            }

            services.RegisterServices();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseMvc();
        }
    }
}
