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
using SeleniumWebDriver.PageObjects;

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
            WebDriverWait webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            IWebElement searchResultElement = webDriverWait.Until(x =>
                x.FindElement(By.XPath("//h2[@class='result-title']/span[contains(text(),'Результаты поиска')]")));

            var searchResultElements =
                driver.FindElements(By.XPath("//div[@class='title-itm']/h5[contains(text(),'MacBook')]"));
            var a = searchResultElements.Select(el => el.Text.Contains(expectedResult)).Distinct();
            //assert
            CollectionAssert.AreEqual(new[] {true}, a,
                "search result does not contain expected result");
        }

        [Test(Description = "Search LG TV via a search filter")]
        public void SearchItemsViaFilters()
        {
            //arrange
            driver.Url = "https://www.citrus.ua";
            var menuTv = driver.FindElement(By.CssSelector("a[title='Телевизоры, фото, видео'] span[class='title']"));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(22);
            Actions action = new Actions(driver);
            action.MoveToElement(menuTv).Perform();

            //act
            var link = driver.FindElement(By.CssSelector("a[title='LG']"));
            var js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("document.querySelector('a[title=\"LG\"]').click()");
            link.Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(22);
            Waiter.WaitForElement(driver, x=>x.FindElement(By.XPath("//div[@class='catalog__main-content']/h1")));
            var actualTitle = driver.FindElement(By.XPath("//a[contains(text(),'lg')]"));
            var result = actualTitle.Text;
            
            var expectedTitle = "lg";
            var webTitle = "LG";
            ////assert

            //Assert.AreEqual(expectedTitle, result, "The title of the page does not contain the expected word");
            Assert.IsTrue(driver.Title.Contains(webTitle));
        }

        [Test(Description = "Verify a TV name and price")]
        public void TvNamePrice()
        {
            //arrange
            driver.Url = "https://www.citrus.ua/televizory/brand-lg";
            var searchedTvNames =
                driver.FindElements(By.XPath("//div[@itemprop='offers']//div[@class='product-card__name']"));
            var searchedTvPrices =
                driver.FindElements(By.XPath("//div[@itemprop='offers']//div[@class='prices__price']"));
            //act
            var expectedItemName = searchedTvNames.First().Text;
            var expectedItemPrice = searchedTvPrices.First().Text;
            searchedTvNames.FirstOrDefault()?.Click();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(12);

            //assert
            Waiter.WaitForElement(driver, x =>
                x.FindElement(By.XPath("//div[@class='el-tabs__nav']/div[@class='el-tabs__item is-active']")));

            var actualPrice =
                driver.FindElement(By.XPath("//div[@class='normal__prices']/div[@class='price']"));
            var price = actualPrice.Text.Replace("\r\n", " ");
            var actualName =
                driver.FindElement(By.XPath("//header[@class='product__header']/h1"));
            var name = actualName.Text;

            Assert.AreEqual(expectedItemName, name);
            Assert.AreEqual(expectedItemPrice, price);
        }

        [Test(Description = "Verify filter's order ")]
        public void FiterOrder()
        {
            //arrange
            var filterList = new List<string>();
            filterList.Add("Цена");
            filterList.Add("Бренд");
            filterList.Add("Диагональ");
            filterList.Add("Разрешение");
            filterList.Add("Тип матрицы");
            filterList.Add("Smart TV");
            filterList.Add("Суммарная мощность динамиков");
            filterList.Add("Операционная система");
            filterList.Add("Цифровой ТВ-тюнер");
            filterList.Add("Поддержка Wi-Fi");

            //act
            driver.Url = "https://www.citrus.ua/televizory/brand-lg";
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            var filter = driver.FindElements(By.XPath("//div[@class='filter-itm__title']")).Select(x => x.Text)
                .ToList();

            //assert
            CollectionAssert.AreEqual(filterList, filter);
        }

        
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
        
    }
}
