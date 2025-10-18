using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BddFrameworkVs.Utlities
{
    public class CommonFunctions
    {
        private readonly ScenarioContext _scearioContext;
        private readonly WebDriverWait _wait;
        private readonly IWebDriver _driver;
        private readonly LocatorReader _commonLocatorReader;
        private LocatorReader _moduleLocatorReader;
        private readonly LibFunctions _libFunctions;
        public string url;
        public string moduleGrid;
        public string moduleObject;
        public bool bExitFlag;
        private By columName;
        public ExtentTest Feature, Scenario;

        public CommonFunctions(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _commonLocatorReader = new LocatorReader(@"..\\..\\");
            _libFunctions = new LibFunctions(driver);

        }

        public void LaunchApplication(string url)
        {
            if(DriverManager.IsAlive())
            { 
                DriverManager.kill();
            }

            IWebDriver driver = DriverFactory.InitDriver(ConfigReader.GetBrowser());
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            DriverManager.Driver = driver;  
            WaitHelper.ElementIsVisible(driver, By.TagName("body"), timeout:TimeSpan.FromSeconds(100));
        }

        public void VerifColumns(string columns)
        {
            var missingColumns = new List<string>();

            string[] columnArray = columns.Split(',');
            foreach (string column in columnArray)
            {
                columName = By.XPath($"//th[normalize-space()='{column.Trim()}']");
                IWebElement columnElement = WaitHelper.ElementIsVisible(_driver, columName, timeout: TimeSpan.FromSeconds(5));
                if(columnElement != null)
                {
                    ExtentHelper.CurrentSceario.Pass($"Column '{column}' is present in the table.");
                }
                else
                {
                    ExtentHelper.CurrentSceario.Fail($"Column '{column}' is present in the table.");
                    missingColumns.Add(column.Trim());
                }
            }
        }
    }
}
