﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AlexisCorePro.Business.Auth.Command;
using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using static AlexisCorePro.Web.Controllers.AccountController;

namespace IntegrationTests
{
    public class TestHelper
    {
        private static HttpClient httpClient;

        private static string Token { get; set; }

        public static HttpClient GetClient()
        {
            if (httpClient == null)
            {
                var projectDir = Directory.GetCurrentDirectory();

                IWebHostBuilder builder = new WebHostBuilder()
                  .UseEnvironment("Development")
                  .UseContentRoot(projectDir)
                  .UseConfiguration(new ConfigurationBuilder()
                      .SetBasePath(projectDir)
                      .AddJsonFile("appsettings.json")
                      .Build()
                  )
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
                });

                TestServer testServer = new TestServer(builder);

                httpClient = testServer.CreateClient();
            }

            return httpClient;
        }

        public static string GetToken()
        {
            HttpResponseMessage httpResponse = httpClient.PostAsJsonAsync("api/account/login", new LoginCommand
            {
                Email = "admin@gmail.com",
                Password = "Admin123!"
            }).Result;

            if (Token == null)
            {
                Token = GetResponseContent<Response<string>>(httpResponse).Data;
            }

            return Token;
        }

        public static T GetResponseContent<T>(HttpResponseMessage response)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public static string GetStringContent(HttpResponseMessage response)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }

        [AssemblyInitialize()]
        public static void TestInitialize(TestContext testContext)
        {

        }
    }
}
