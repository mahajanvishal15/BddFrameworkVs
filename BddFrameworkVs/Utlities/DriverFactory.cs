using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BddFrameworkVs.Utlities
{
    public static class DriverFactory
    {
        public static IWebDriver InitDriver(string browser)
        {
         return browser.ToLower() switch
         { "chrome" => new OpenQA.Selenium.Chrome.ChromeDriver(),
           "firefox" => new OpenQA.Selenium.Firefox.FirefoxDriver(),
           "edge" => new OpenQA.Selenium.Edge.EdgeDriver(),
           _ => throw new ArgumentException($"Unsupported browser: {browser}"),
         };
        }

        private static string GetDriverPath(string driverName)
        {
            string driverDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Drivers");
            Console.WriteLine($"Driver Directory: {driverDirectory}");
            if(!Directory.Exists(driverDirectory))
            {
                Directory.CreateDirectory(driverDirectory);
            }

            return driverDirectory;
        }


    }
}
