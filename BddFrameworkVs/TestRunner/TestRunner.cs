using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using BddFrameworkVs.Utlities;
using OpenQA.Selenium;

namespace BddFrameworkVs.TestRunner
{
    [Binding]
    public class TestRunner
    {

        public static IWebDriver Driver;
        public static string reportFolderPath;
        public static ExtentReports Extent;
        public static ExtentTest Feature, Scenario;
        public static string ReportFolderPath { get; private set; }
        public static CommonFunctions _commonFunctions;
        public static string url;
        public static string moduleGrid;
        private static LocatorReader _moduleLocatorReader;
        public static IWebDriver driver;
        
        
        [BeforeTestRun]
        public static void Init()
        {
            Extent = ExtentManager.CreateInstance();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Feature = Extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(featureContext.FeatureInfo.Title);
        }

    }
}
