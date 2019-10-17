using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumWebDriver.Helper;

namespace SeleniumWebDriver
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void SetUpDriver()
        {
            driver = CreateDriver.Driver;
            driver.Manage().Window.Maximize();
           
        }

        [Test(Description = "Search MacBooks via general search")]

        public void SearchItemByGlobalSearch()
        {
            //arrange
            driver.Url = "https://www.citrus.ua";
            var searchField = driver.FindElement(By.Id("search-input"));
            searchField.SendKeys("macbook");
            var searchButton = driver.FindElement(By.ClassName("el-icon-search"));
            searchButton.Click();
            //act
           
            var expectedResult = "MacBook";
            WebDriverWait webDriverWait = new WebDriverWait(driver,TimeSpan.FromSeconds(3));
            IWebElement searchResultElement = webDriverWait.Until(x =>
                x.FindElement(By.XPath("//h2[@class='result-title']/span[contains(text(),'Результаты поиска')]")));
            
            var searchResultElements = driver.FindElements(By.XPath("//div[@class='title-itm']/h5[contains(text(),'MacBook')]"));
            var a = searchResultElements.Select(el => el.Text.Contains(expectedResult)).Distinct();
            //assert
            CollectionAssert.AreEqual(new[]{ true }, a,
            "search result does not contain expected result");
        }

        [Test(Description = "Search LG TV via a search filter")]

        public void SearchItemsViaFilters()
        {
            //arrange
            driver.Url = "https://www.citrus.ua";
            var menuTv = driver.FindElement(By.CssSelector("a[title='Телевизоры, фото, видео'] span[class='title']"));
            
            Actions action = new Actions(driver);
            action.MoveToElement(menuTv).Perform();


            //action
            //Waiter.WaitForElement(driver, x => x.FindElement(By.XPath("//li[@class='title']/div[contains(text(),'Телевизоры по брендам')]")));
            var linkTv = driver.FindElement(By.CssSelector("a[title='LG'] span"));
            linkTv.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var actualTitle  = driver.Title.Contains("LG");
           
            //var actualTitle = driver.FindElement(By.XPath("//title[contains(text(),'LG')]"));
            var expectedTitle = "LG";
            ////assert
          
            Assert.AreEqual(expectedTitle,actualTitle, "The title of the page does not contain the expected word");
        }

        [Test(Description = "Verify a TV name and price")]
        public void TvNamePrice()
        {
            //arrange
            driver.Url = "https://www.citrus.ua/televizory/brand-lg/";
            
            var searchedTvName = driver.FindElement(By.CssSelector("a[title='LG 43\" 4K Smart TV (43UK6300PLB)']"));
            var searchedTvPrice =
                driver.FindElement(By.XPath("//div[@class='prices__old-price']/span[contains(text(),'16 799')]"));
            
            //action
            searchedTvName.Click();
            //Waiter.WaitForElement(driver, x => x.FindElement(By.XPath("//h1[contains(text(),'LG 43\" 4K Smart TV (43UK6300PLB)')])]")));

            //assert
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            var actualPrice =
                driver.FindElement(By.XPath("//div[@class='buy-block__sales']//span[contains(text(),'16 799')][1]"));
            var actualName =
                driver.FindElement(By.XPath("//h1[contains(text(),'LG 43\" 4K Smart TV (43UK6300PLB)')])]"));

            Assert.AreEqual(searchedTvName, actualName);
            Assert.AreEqual(searchedTvPrice, actualPrice);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        



    }
}
