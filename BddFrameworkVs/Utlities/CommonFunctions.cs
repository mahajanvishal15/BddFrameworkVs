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
    }
}
