using NewPOM.Base;
using NewPOM.Custom;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.UnitTests
{
    //[TestFixture(typeof(ChromeDriver))]
    //[TestFixture(typeof(FirefoxDriver))]
    //[Parallelizable]
    class UnitTest2 :BaseFixture     //<Multi> where Multi : IWebDriver, new()
    {
        public IWebDriver driverContext;

        [SetUp]
        public void Setup()
        {
            driverContext = new FirefoxDriver();
            driverContext.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
            driverContext.Navigate().GoToUrl("https://www.facebook.com/");
        }
        [Test]
        public void Test2()
        {
            var test = ExtentTestCustom.CreateTest("in test 2");
            test.Pass("");
        }

    }
}
