﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
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
       //private readonly IWebDriver _driver;

       //public TestsTask2()
       //{
       //    _driver = CreateDriver.Driver;
       //}

       [OneTimeSetUp]
        public void SetUpDriver()
        {
            CreateDriver.Driver.Manage().Window.Maximize();
        }

        [Test(Description = "Open a page in a new tab")]
        public void OpenNewTab()
        {
            //arrange
            var driver = CreateDriver.Driver;
            var navigationMenuToolsQaPage = new NavigationMenuToolsQAPage(driver);
            driver.Url = "http://toolsqa.com";
            var automationSwitchedWindowsPage =navigationMenuToolsQaPage.NavigateToPageFromMenu(navigationMenuToolsQaPage.DemoSites, driver);
            
            //act
            automationSwitchedWindowsPage.SubmitNewBrowserButton(automationSwitchedWindowsPage.NewBrowserButton);
            automationSwitchedWindowsPage.SwitchDriverToNewTab();

            //Assert
            var expectedTitle = "QA Automation Tools Tutorial";
             Assert.IsTrue(driver.Title.Contains(expectedTitle));
        }

        [Test(Description = "Alert")]
        public void AlertVerification()
        {
            //arrange
            var driver = CreateDriver.Driver;
            var automationSwitchedWindowsPage = new AutomationSwitchedWindowsPage(driver);

            driver.Url = "http://toolsqa.com/automation-practice-switch-windows/";

            //act
            automationSwitchedWindowsPage.InvokeAlert(automationSwitchedWindowsPage.Alert, automationSwitchedWindowsPage.TimingAlert);
            var wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait1.Until(drv => automationSwitchedWindowsPage.IsAlertShown(drv));
            IAlert alert1 = driver.SwitchTo().Alert();

            var actualText = alert1.Text;
            //assert
            var expectedText =
                 "Knowledge increases by sharing but not by saving. Please share this website with your friends and in your organization.";

            Assert.AreEqual(expectedText, actualText);

        }

        [Test(Description = "iFrame")]
        public void IFrame()
        {
            //arrange
            var driver = CreateDriver.Driver;
            driver.Url = "https://www.w3schools.com/hTml/html_iframe.asp";
            var w3SchoolFramesPage = new W3SchoolFramesPage(driver);
            var iFrame = driver.SwitchTo().Frame(w3SchoolFramesPage.Frame);

            w3SchoolFramesPage.ScrollToButtonInFrameAndClick(w3SchoolFramesPage.HeaderInFrame, w3SchoolFramesPage.ButtonNext);

            //assert
            var expectedIframeTitle = "Introduction";
            var expectedtabTitle = "HTML Iframes";
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(dr => iFrame.FindElement(By.XPath("//h1/span")).Text);

            var iframeActualTitle = iFrame.FindElement(By.XPath("//h1/span")).Text;

            var tabActualTitle = driver.Title;
            Assert.AreEqual(expectedIframeTitle, iframeActualTitle);
            Assert.AreEqual(expectedtabTitle, tabActualTitle);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            CreateDriver.Driver.Quit();
            CreateDriver.Driver.Dispose();
        }
    }
}

