using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;

namespace BddFrameworkVs.Utlities
{
    public static class ExtentManager
    {
        private static IConfigurationRoot config;
        public static string basePath;
        public static string testReultsPath;

        static ExtentManager()
        {
            string workspace = Environment.GetEnvironmentVariable("WORKSPACE");
            basePath = workspace != null 
                ? Path.Combine(workspace, "TestResults") 
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestResults");
        }

        public static ExtentReports CreateInstance()
        {
            var extent = new ExtentReports();
            testReultsPath = Path.Combine(basePath, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}");
            Directory.CreateDirectory(testReultsPath);
            var htmlReporter = new ExtentSparkReporter(Path.Combine(testReultsPath, $"AutomationReport_{DateTime.Now:yyyMMdd_HHmmss}.html"));
            extent.AttachReporter(htmlReporter);
            return extent;
        }
    }
}
