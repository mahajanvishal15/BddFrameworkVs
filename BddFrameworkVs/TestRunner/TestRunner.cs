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
            Feature = Extent.CreateTest(featureContext.FeatureInfo.Title);

            var tags = featureContext.FeatureInfo.Tags; //Reading theTags associated with the feature

            bool urlFlag = true;
            foreach (var tag in tags)
            {
                if (tag.Contains("URL"))
                {
                    string urlName = tag.Split("=")[1];
                    if (DriverManager.IsAlive())
                    {
                        DriverManager.kill();
                    }

                    var envData = ConfigReader.GetEnviornmentsData();

                    switch (urlName.ToUpper())
                    {
                        case "DEV":
                            url = envData["DevUrl"];
                            moduleGrid = envData["DevModuleGrid"];
                            break;
                        case "QA":
                            url = envData["QaUrl"];
                            moduleGrid = envData["QaModuleGrid"];
                            break;
                        case "UAT":
                            url = envData["UatUrl"];
                            moduleGrid = envData["UatModuleGrid"];
                            break;
                        default:
                            urlFlag = false;
                            break;
                    }

                    driver = DriverFactory.InitDriver(ConfigReader.GetBrowser());
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl(url);
                    DriverManager.Driver = driver;
                    urlFlag = false;
                    break;
                }
            }

            if (urlFlag)
            {
                driver = DriverFactory.InitDriver(ConfigReader.GetBrowser());
                driver.Manage().Window.Maximize();
                DriverManager.Driver = driver;
            }
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Scenario = Feature.CreateNode(scenarioContext.ScenarioInfo.Title);
            ExtentHelper.CurrentSceario = Scenario;
        }

        [AfterStep]
        public void AfterEachStep(ScenarioContext scenarioContext)
        {
            var driver = DriverManager.Driver;
            if (scenarioContext.TestError != null)
            {
                var screenshot = ScreenshotHelper.TakeScreenshot(driver);
                Scenario.Fail(scenarioContext.StepContext.StepInfo.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());

            }
            else
            {
                var screenshot = ScreenshotHelper.TakeScreenshot(driver);
                Scenario.Pass(scenarioContext.StepContext.StepInfo.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());

            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    var screenshot = ScreenshotHelper.TakeScreenshot(driver);
                    Scenario.Fail(scenarioContext.StepContext.StepInfo.Text, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshot).Build());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while quitting driver: {ex.Message}");
                Scenario.Fail(ex.Message.ToUpper());
            }
            finally
            {
                //DriverManager.kill();
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            try
            {
                DriverManager.Driver?.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while quitting driver: {ex.Message}");
            }
            finally
            {
                DriverManager.Driver = null;
            }
        }

        [AfterTestRun]
        public static void TearDown()
        {
            Extent.Flush();
        }

    }
}
