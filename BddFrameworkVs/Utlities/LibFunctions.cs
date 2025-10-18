using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BddFrameworkVs.Utlities
{
    public class LibFunctions
    {
        private readonly WebDriverWait _wait;
        private readonly IWebDriver _driver;

        public LibFunctions(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void WindowHandles()
        {
            var driver = DriverManager.Driver;
            
            String ParentHandle = driver.CurrentWindowHandle;
            var handles = driver.WindowHandles;
            foreach (var handle in handles)
            {
                Console.WriteLine($"Window Handle: {handle}");
                if (handle != ParentHandle)
                {
                    driver.SwitchTo().Window(handle);
                }
            }
        }

        public void ClickButton(By elementByPath)
        {

        }

    }
}
