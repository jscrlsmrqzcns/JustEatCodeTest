using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustEatCodeTestWeb.Tests.UI
{
    [TestClass]
    public class SearchUITest
    {
        private static RemoteWebDriver _driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            // TODO start web, at the moment has to be manually started
            _driver = new ChromeDriver("."); // chromedriver.exe copied to output directory = selfcontained

        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _driver.Close();
            _driver.Dispose();
        }

        [TestMethod]
        public void SearchTermIsRequired()
        {
            _driver.Navigate().GoToUrl("http://localhost:50773/JustEatCodeTest/"); // TODO refactor to test framework classes
            var searchBtn = _driver.FindElement(By.Id("search-btn"));
            Assert.IsFalse(searchBtn.Enabled, "button enabled when there is search term"); 
            var searchInput = _driver.FindElementById("search-input");
            searchInput.SendKeys("anything");
            Assert.IsTrue(searchBtn.Enabled, "button disabled when there is no search term");
        }

        [TestMethod]
        public void ResultsForSE19()
        {
            _driver.Navigate().GoToUrl("http://localhost:50773/JustEatCodeTest/"); // TODO refactor to test framework classes
            var searchInput = _driver.FindElementById("search-input");
            searchInput.SendKeys("SE19");
            var searchBtn = _driver.FindElement(By.Id("search-btn"));
            searchBtn.Click();
            Assert.IsFalse(searchBtn.Enabled);
            var wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(15));
            var resutsTable = wait.Until(drv => drv.FindElement(By.Id("table-serch-results")));
            var resultsTableBody = wait.Until(drv => drv.FindElement(By.TagName("tbody")));
            var resultRows = wait.Until(drv => drv.FindElements(By.TagName("tr")));
            Assert.IsTrue(resultRows.Count > 1, "at least one result");
        }


    }
}
