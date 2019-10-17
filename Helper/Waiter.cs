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
            WebDriverWait webDriverWait = new WebDriverWait(superDriver, TimeSpan.FromSeconds(5));
            IWebElement linkTvElement = webDriverWait.Until(condition);
        }
    }
}
