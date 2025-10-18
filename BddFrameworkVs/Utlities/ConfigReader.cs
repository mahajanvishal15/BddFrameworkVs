using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BddFrameworkVs.Utlities
{
    public static class ConfigReader
    {
        private static IConfigurationRoot config;
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"..\..\..\Configuration\");

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(FilePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config = builder.Build();
        }

        public static string GetEnviorment() => config["Environment"];
        public static string GetBrowser() => config["Browser"];

        public static string GetUrl()
        {
            var env=GetEnviorment();
            return config[$"Enviornments:{env}"];
        }

        public static IConfigurationSection GetEnviornmentData()
        {
            var env = GetEnviorment();
            return config.GetSection($"EnviornmentsData:{env}");
        }

        public static Dictionary<string,string> GetEnviornmentsData()
        {
            var env = GetEnviorment(); 
            var envSection = config.GetSection($"EnviornmentsData:{env}");
            var data = new Dictionary<string, string>();
            foreach (var child in envSection.GetChildren())
            {
                data[child.Key] = child.Value;
            }
            return data;
        }
    }
}
