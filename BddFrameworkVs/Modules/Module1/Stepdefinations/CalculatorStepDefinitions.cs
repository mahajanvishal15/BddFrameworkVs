using BddFrameworkVs.Utlities;
using OpenQA.Selenium;

namespace BddFrameworkVs.Modules.Module1.Stepdefinations
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef
        private By columName;
        private bool exitFlag;
        private int scrollLenghth;

        [Given("the first number is {int}")]
        public void GivenTheFirstNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see https://go.reqnroll.net/doc-sharingdata
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 

            throw new PendingStepException();
        }

        [Given("the second number is {int}")]
        public void GivenTheSecondNumberIs(int number)
        {
            //TODO: implement arrange (precondition) logic

            throw new PendingStepException();
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic

            throw new PendingStepException();
        }

        [Then("the result should be {int}")]
        public void ThenTheResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            throw new PendingStepException();
        }

        [Given("the following numbers exist:")]
        public void GivenTheFollowingNumbersExist(DataTable dataTable)
        {
            Console.WriteLine($" databe row count : {dataTable.Rows.Count}");
            var missingValue = new List<string>();
            IWebElement horizontalScrollBar = DriverManager.Driver.FindElement(By.XPath("//div[@class='ag-body-horizontal-scroll']"));

            var js = (IJavaScriptExecutor)DriverManager.Driver;
            long clientWidht = (long)js.ExecuteScript("return arguments[0].clientWidth;", horizontalScrollBar);
            long scrollWidth = (long)js.ExecuteScript("return arguments[0].scrollWidth;", horizontalScrollBar);
            long maxWidth = scrollWidth + clientWidht;
            foreach (var header in dataTable.Header)
            {
                Console.WriteLine($" Header : {header}");

                List<string> columNames = dataTable.Rows.Select(r => r[header]).ToList();

                for (int i = 0; i < columNames.Count; i++)
                {
                    columName = By.XPath("");
                    exitFlag = false;
                    scrollLenghth = 0;

                    do
                    {
                        IWebElement colElement = WaitHelper.ElementIsVisible(DriverManager.Driver, columName, timeout: TimeSpan.FromSeconds(2));
                        if (colElement != null && colElement.Displayed)
                        {
                            exitFlag = true;
                            js.ExecuteScript($"arguments[0].scrollLeft -= '{scrollLenghth}'", colElement);
                        }
                        else
                        {
                            scrollLenghth += 150;
                            Console.WriteLine($" Scrolling to find column : {columNames[i]}");
                            js.ExecuteScript($"arguments[0].scrollLeft += '{scrollLenghth}'", horizontalScrollBar);

                        }

                    } while (exitFlag = false || scrollLenghth < scrollWidth);

                    if (exitFlag)
                    {
                        ExtentHelper.CurrentSceario.Pass($"Column '{columNames[i]}' is present in the table.");

                    }
                    else
                    {
                        missingValue.Add(columNames[i]);
                        js.ExecuteScript($"arguments[0].scrollLeft -= '{scrollLenghth}'", horizontalScrollBar);
                    }
                }

                if (missingValue.Count > 0)
                {
                    ExtentHelper.CurrentSceario.Fail($"Columns '{string.Join(", ", missingValue)}' are missing in the table.");
                }
            }

        }
    }
}
