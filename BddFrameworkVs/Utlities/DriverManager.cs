using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BddFrameworkVs.Utlities
{
    public class DriverManager
    {
        public static IWebDriver Driver { get; set; }

        public static bool IsAlive()
        {
            if (Driver == null)
                return false;

            try
            {
                var _driver = Driver.WindowHandles;
                return true;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        public static void kill()
        {
            try
            {
                Drvier?.Quit();
            }
            catch (Exception ex)
            {
                // Ignore exceptions during driver quit
                Console.WriteLine($"Exception while quitting driver: {ex.Message}");
            }
            finally
            {
                if (Driver is IDisposable d) d.Dispose();
                Driver = null;
            }
        }
    }
}
