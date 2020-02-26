using HolidayOptimizer.Api.Domain;
using HolidayOptimizer.Api.Services.Interfaces;
using HolidayOptimizer.Api.Tests.Helpers;
using HolidayOptimizer.Api.Tests.IntegrationTests.HttpMessageHandlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace HolidayOptimizer.Api.Tests.IntegrationTests.Helpers
{
    public static class ClientFactory
    {
        public static HttpClient Create()
        {            
            var projectDir = GetProjectPath("", typeof(Startup).GetTypeInfo().Assembly);

            var cacheService = Substitute.For<ICacheService>();

            cacheService.Get<Holidays>($"holidays_{DateTime.UtcNow.Year}").Returns(HolidaysFactory.Create());            

            var webHost = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(projectDir)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build())
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton<ICacheService>(cacheService);
                    services.AddSingleton<HttpClient>(new HttpClient(new GetHolidaysHttpMessageHandler()));
                })
                .UseStartup<Startup>();

            var testserver = new TestServer(webHost);

            return testserver.CreateClient();
        }

        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;

            var applicationBasePath = AppContext.BaseDirectory;

            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));
                if (projectDirectoryInfo.Exists)
                {
                    var projectFileInfo = new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj"));
                    if (projectFileInfo.Exists)
                    {
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
                    }
                }
            }
            while (directoryInfo.Parent != null);

            throw new InvalidOperationException($"Project root could not be located using the application root {applicationBasePath}.");
        }
    }
}
