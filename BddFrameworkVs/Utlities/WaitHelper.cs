using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BddFrameworkVs.Utlities
{
    public class WaitHelper
    {
        public static T Until<T>(
            IWebDriver driver,
            Func<IWebDriver, T> condition,
            TimeSpan? timeout=null,
            TimeSpan? pollingInterval= null,
            IEnumerable<Type> ignoredExceptions = null)
        {
            var wait = new DefaultWait<IWebDriver>(driver);
            {
                //Timeout = timeout ?? TimeSpan.FromSeconds(30),
                //PollingInterval = pollingInterval ?? TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            if(ignoredExceptions != null)
                foreach ( var exc in ignoredExceptions)
                    wait.IgnoreExceptionTypes(exc);

            return wait.Until(condition);

        }

        public static IWebElement ElementIsVisible(
            IWebDriver driver,
            By by,
            TimeSpan? timeout = null,
            TimeSpan? pollingInterval = null
            )
        {
            return Until(driver,
                ExpectedConditions.ElementIsVisible(by),
                timeout,
                pollingInterval);
        }

    }
}
