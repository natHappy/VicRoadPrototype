using NewPOM.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NewPOM.Steps
{
    [Binding]
    public class NavigatingSteps
    {
        private IWebDriver _driver;
        public NavigatingSteps(IWebDriver driver)
        {
            _driver = driver;
        }
        [Then(@"The ProgressTitle of next page is displayed")]
        public void ThenTheProgressTitleOfNextPageIsDisplayed(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            string title = (string)data.PageTitle;
            Assert.That(_driver.IsProgressBarTitleDisplayed("Step 2 of 7 : Select permit type"), Is.True, "Wrong Page");
        }
    }
}
