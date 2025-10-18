using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BddFrameworkVs.Utlities
{
    public static class ScreenshotHelper
    {

        public static string TakeScreenshot(IWebDriver driver)
        {
            try
            {
                if(driver == null || ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString() != "complete")
                {
                    Console.WriteLine("Driver is null or page is not fully loaded. Cannot take screenshot.");
                    return null;
                }
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                string screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                return screenshotPath;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
                return null;
            }
        }
    }
}
