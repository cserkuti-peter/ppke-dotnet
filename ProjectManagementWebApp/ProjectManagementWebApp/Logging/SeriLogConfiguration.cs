using System;

using Microsoft.Extensions.Configuration;

using Serilog;

namespace ProjectManagementWebApp.Logging
{
    public static class SeriLogConfiguration
    {
        private static string CreateEnvironmentNameVariable()
        {
            return $@"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json";
        }

        private static IConfigurationRoot CreateSeriLogConfiguration()
        {
            return new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                         .AddJsonFile(CreateEnvironmentNameVariable(), optional: true)
                         .Build();
        }

        public static void CreateLoggerConfiguration()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(CreateSeriLogConfiguration())
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
