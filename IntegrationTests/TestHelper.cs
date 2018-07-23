﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IntegrationTests
{
    public class TestHelper
    {
        private static HttpClient httpClient;

        public static HttpClient GetClient()
        {
            if (httpClient == null)
            {
                IWebHostBuilder builder = new WebHostBuilder()
                  .UseEnvironment("Development")
                  .UseStartup<AlexisCorePro.Startup>();

                builder.ConfigureServices(services =>
                {
                    ServiceProvider serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        options.UseInMemoryDatabase("AlexisPro");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    ServiceProvider sp = services.BuildServiceProvider();

                    using (IServiceScope scope = sp.CreateScope())
                    {
                        IServiceProvider scopedServices = scope.ServiceProvider;
                        DatabaseContext db = scopedServices.GetRequiredService<DatabaseContext>();
                    }
                });

                TestServer testServer = new TestServer(builder);

                httpClient = testServer.CreateClient();
            }

            return httpClient;
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            string responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
