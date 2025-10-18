using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BddFrameworkVs.Utlities
{
    public class JsonTestDataReader
    {
        private static IConfigurationRoot _jsonDataPathReader;
        private static string FilePath;
        private static string fileName;

        public JsonTestDataReader(string moduleName)
        {
            switch (moduleName.ToUpper())
                {
                case "USERMANAGEMENT":
                    FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestData\UserManagementTestData.json");
                    fileName = "UserManagementTestData.json";
                    break;
                case "PRODUCTCATALOG":
                    fileName = "ProductCatalogTestData.json";
                    break;
                default:
                    throw new ArgumentException($"Unsupported module: {moduleName}");
            }

            _jsonDataPathReader = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(FilePath))
                .AddJsonFile(fileName, optional: false, reloadOnChange: true)
                .Build();
        }

        public Dictionary<string,string> GetJsonTestData()
        {
            var data = new Dictionary<string, string>();
            foreach (var child in _jsonDataPathReader.GetChildren())
            {
                data[child.Key] = child.Value;
            }
            return data;
        }

    }
}
