using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumWebDriver.PageObjects
{
    public class NavigationMenuToolsQAPage
    {
        private readonly IWebDriver _driver;

        public NavigationMenuToolsQAPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement DemoSites => _driver.FindElement(By.XPath("//nav[@class='navigation']//span[contains(text(),'DEMO SITES')]"));

        private IWebElement AutoSwitchedWindow => _driver.FindElement(
            By.XPath("//nav[@class='navigation']//span[contains(text(),'Automation Practice Switch Windows')]"));

        public AutomationSwitchedWindowsPage NavigateToPageFromMenu(IWebElement element, IWebDriver _driver)
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(element).Perform();
            AutoSwitchedWindow.Click();
            return new AutomationSwitchedWindowsPage(_driver);
        }

    }
}
