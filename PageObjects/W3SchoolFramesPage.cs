using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SeleniumWebDriver.PageObjects
{
    public class W3SchoolFramesPage
    {
        private readonly IWebDriver _driver;

        public W3SchoolFramesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Frame => _driver.FindElement(By.XPath("//iframe[@src='default.asp']"));
        public IWebElement ButtonNext => _driver.FindElement(By.XPath("//a[@class='w3-right w3-btn']"));
        public IWebElement HeaderInFrame => _driver.FindElement(By.XPath("//h1/span[contains(text(),'Tutorial')]"));

        public void ScrollToButtonInFrameAndClick(IWebElement element1, IWebElement element2)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", element1);
            element2.Click();
        }
    }
}
