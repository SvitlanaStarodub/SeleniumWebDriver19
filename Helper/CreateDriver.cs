using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriver
{
    public static class CreateDriver
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get {
                if (_driver == null)
                {
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);

                    _driver = new ChromeDriver(@"C:\Users\Svitlana_Starodub\source\repos\SeleniumWebDriver", chromeOptions);
                }
                return _driver;
            }
            set => _driver = value;
        }
    }
}
