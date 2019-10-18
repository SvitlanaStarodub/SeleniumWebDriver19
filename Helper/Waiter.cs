using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver.Helper
{
    public static class Waiter
    {
        public static void WaitForElement(IWebDriver superDriver, Func<IWebDriver, IWebElement> condition)
        {
            WebDriverWait webDriverWait = new WebDriverWait(superDriver, TimeSpan.FromSeconds(2));
            IWebElement element = webDriverWait.Until(condition);
        }

        public static bool returnDisplayedElement(IWebDriver driver, string xpath)
        {
            try
            {
                var element = driver.FindElement(By.XPath(xpath));
                return element.Enabled;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
