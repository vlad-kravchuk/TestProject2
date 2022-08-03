using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_Demo
{
    class Selenium_Demo
    {

        IWebDriver driver;

        [SetUp]
        public void start_Browser()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
        }

        [Test]
        public void test_search()
        {
            // implicit wait 30 sec
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            // navigate to Google
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Manage().Window.Maximize();

            // insert a keyword to search and open an image search section
            var searchText = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            searchText.SendKeys("german shepherd");
            searchText.SendKeys(Keys.Enter);
            var clickImageSection = driver.FindElement(By.XPath("/html/body/div[7]/div/div[4]/div/div[1]/div/div[1]/div/div[2]/a"));
            clickImageSection.Click();

            // click on immage to verify, if it contains the images
            IWebElement clickImages = driver.FindElement(By.XPath($"/html/body/div[2]/c-wiz/div[3]/div[1]/div/div/div/div[1]/div[1]/span/div[1]/div[1]/div[1]/a[1]/div[1]/img")); ;

                try
                {
                    clickImages.Click();
                    clickImages = driver.FindElement(By.XPath($"/html/body/div[2]/c-wiz/div[3]/div[1]/div/div/div/div[1]/div[1]/span/div[1]/div[1]/div[1]/a[1]/div[1]/img"));
                }
                catch (ArgumentNullException e) when (clickImages == null) { }
          

        }

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }
}