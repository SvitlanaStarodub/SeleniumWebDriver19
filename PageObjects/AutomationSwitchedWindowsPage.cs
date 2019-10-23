using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriver.PageObjects
{
    public class AutomationSwitchedWindowsPage
    {
        private readonly IWebDriver _driver;

        public AutomationSwitchedWindowsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement NewBrowserButton =>
            _driver.FindElement(By.XPath("//button[contains(text(),'New Browser Tab')]"));

        public IWebElement TimingAlert => _driver.FindElement(By.XPath("//button[contains(text(),'Timing Alert')]"));
        public IWebElement Alert => _driver.FindElement(By.Id("alert"));
        
        public void SubmitNewBrowserButton(IWebElement element)
        {
             element.Submit();
        }

        public void SwitchDriverToNewTab()
        {
            var oldTab = _driver.CurrentWindowHandle;
            var handles = _driver.WindowHandles;
            _driver.SwitchTo().Window(handles.Last());
        }

        public void InvokeAlert(IWebElement element1, IWebElement element2)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", element1);
            element2.Click();
        }

        public bool IsAlertShown(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException e)
            {
                return false;
            }

            return true;
            
            
        }
        
    }
}
