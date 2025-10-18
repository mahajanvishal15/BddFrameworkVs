using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace BddFrameworkVs.Utlities
{
    public class LocatorReader
    {

        private readonly JObject _locators;

        public LocatorReader(string relativePath)
        {
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath, "Configuration", "locators.json");
            var jsonContent = System.IO.File.ReadAllText(filePath);
            _locators = JObject.Parse(jsonContent);
        }

        public By GetLocator(string pageName,string elementName)
        {
            var element = _locators[pageName]?[elementName];
            var type = element?["type"]?.ToString();
            var value = element?["value"]?.ToString();

            return type.ToLower() switch
            { 
                "id" => By.Id(value),
                "name" => By.Name(value),
                "xpath" => By.XPath(value),
                "cssselector" => By.CssSelector(value),
                "classname" => By.ClassName(value),
                "tagname" => By.TagName(value),
                "linktext" => By.LinkText(value),
                "partiallinktext" => By.PartialLinkText(value),
                _ => throw new ArgumentException($"Unsupported locator type: {type}"),
            };

        }
    }
}
