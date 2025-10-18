using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BddFrameworkVs.Utlities
{
    public static class ExcelReader
    {
        private static Dictionary<string, Dictionary<string, string>> outerMap;
        private static readonly string ExcelFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\TestData\TestData.xlsx");

        public static void ReadDataFromExcel(string sheetName)
        {
            using (var package = SpreadsheetDocument.Open(ExcelFilePath, false))
            {
                var workbookPart = package.WorkbookPart;
                var sheet = workbookPart.Workbook.Sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>()
                    .FirstOrDefault(s => s.Name == sheetName);
                if (sheet == null)
                    throw new ArgumentException($"Sheet {sheetName} not found in Excel file.");
                var worksheetPart = (DocumentFormat.OpenXml.Packaging.WorksheetPart)workbookPart.GetPartById(sheet.Id);
                var sheetData = worksheetPart.Worksheet.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.SheetData>();

                outerMap = new Dictionary<string, Dictionary<string, string>>();

                int rowCount = sheetData.Elements<Row>().Count();
                int colCount = sheetData.Elements<Row>().First().Elements<Cell>().Count();

                for (int i = 2 ; i <= rowCount; i++)
                {
                    var innerMap = new Dictionary<string, string>();
                    for (int j = 1; j <= colCount; j++)
                    {
                        string key = GetCellValue(sheetData, 1, j).Trim();
                        string value = GetCellValue(sheetData, i, j).Trim();
                        innerMap[key] = value;
                    }
                    string testCaseName = GetCellValue(sheetData,i,1,workbookPart).Trim();
                    outerMap[$"row{i - 1}"] = innerMap;
                }

                foreach(var entry in outerMap)
                {
                    Console.WriteLine($"Test Case: {entry.Key}");
                    foreach(var field in entry.Value)
                    {
                        Console.WriteLine($"{field.Key}: {field.Value}");
                    }
                }


            }

        }

        public static string AccessTestData(string testCaseName,string fieldName)
        {
            if(outerMap.ContainsKey(testCaseName) && outerMap[testCaseName].ContainsKey(fieldName))
            {
                return outerMap[testCaseName][fieldName];
            }
            else
            {
                throw new ArgumentException($"Test case {testCaseName} or field {fieldName} not found.");
                return null;
            }
        }

        private static string GetCellValue(SheetData sheetData, int rowIndex, int colIndex, WorkbookPart workbookPart = null)
        {
            var row = sheetData.Elements<Row>().ElementAt(rowIndex - 1);
            var cell = row.Elements<Cell>().ElementAt(colIndex - 1);
            if (cell == null || cell.CellValue == null) return string.Empty;
            string value = cell.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString && workbookPart != null)
            {
                var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                if (stringTable != null)
                {
                    value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                }
            }
            return value;
        }



    }
}
