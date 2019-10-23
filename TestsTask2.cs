using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumWebDriver.PageObjects;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class TestsTask2

    {
        private readonly IWebDriver _driver;
        private readonly NavigationMenuToolsQAPage _navigationMenuToolsQaPage;
        private readonly AutomationSwitchedWindowsPage _automationSwitchedWindowsPage;
        private readonly W3SchoolFramesPage _w3SchoolFramesPage;

        public TestsTask2()
        {
            _driver = CreateDriver.Driver;
            _navigationMenuToolsQaPage = new NavigationMenuToolsQAPage(_driver);
            _automationSwitchedWindowsPage = new AutomationSwitchedWindowsPage(_driver);
            _w3SchoolFramesPage = new W3SchoolFramesPage(_driver);
        }

        [OneTimeSetUp]
        public void SetUpDriver()
        {
            _driver.Manage().Window.Maximize();
        }

        [Test(Description = "Open a page in a new tab")]
        public void OpenNewTab()
        { 
            //arrange
            _driver.Url = "http://toolsqa.com";
            _navigationMenuToolsQaPage.NavigateToPageFromMenu(_navigationMenuToolsQaPage.DemoSites, _driver);
            
            //act
            _automationSwitchedWindowsPage.SubmitNewBrowserButton(_automationSwitchedWindowsPage.NewBrowserButton);
            _automationSwitchedWindowsPage.SwitchDriverToNewTab();

            //Assert
            var expectedTitle = "QA Automation Tools Tutorial";
             Assert.IsTrue(_driver.Title.Contains(expectedTitle));
        }

        [Test(Description = "Alert")]
        public void AlertVerification()
        {
            //arrange
            _driver.Url = "http://toolsqa.com/automation-practice-switch-windows/";
            
            //act
            
            _automationSwitchedWindowsPage.InvokeAlert(_automationSwitchedWindowsPage.Alert,_automationSwitchedWindowsPage.TimingAlert);
            var wait1 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait1.Until(drv => _automationSwitchedWindowsPage.IsAlertShown(drv));
            IAlert alert1 = _driver.SwitchTo().Alert();

            var actualText = alert1.Text;
            //assert
            var expectedText =
                 "Knowledge increases by sharing but not by saving. Please share this website with your friends and in your organization.";
            

            Assert.AreEqual(expectedText,actualText);

        }

        [Test(Description = "iFrame")]
        public void IFrame()
        {
            //arrange
            _driver.Url = "https://www.w3schools.com/hTml/html_iframe.asp";
            var iFrame = _driver.SwitchTo().Frame(_w3SchoolFramesPage.Frame);
            _w3SchoolFramesPage.ScrollToButtonInFrameAndClick(_w3SchoolFramesPage.HeaderInFrame, _w3SchoolFramesPage.ButtonNext);
            //iFrame.FindElement(By.XPath("//a[@class='w3-right w3-btn']")).Click();

            //assert
            var expectedIframeTitle = "Introduction";
            var expectedtabTitle = "HTML Iframes";
            _driver.Navigate().Refresh();
            var iframeActualTitle = iFrame.FindElement(By.XPath("//h1/span")).Text;
            
            var tabActualTitle = _driver.Title;
            Assert.AreEqual(expectedIframeTitle,iframeActualTitle);
            Assert.AreEqual(expectedtabTitle,tabActualTitle);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}

