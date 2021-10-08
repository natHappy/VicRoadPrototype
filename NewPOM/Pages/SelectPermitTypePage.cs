using NewPOM.Bases;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Pages
{
    class SelectPermitTypePage
    {
        private IWebDriver _driver;
        public SelectPermitTypePage(IWebDriver driver)
        {
            _driver = driver;
        }
        IWebElement radioBtnSingleTrip => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "PermitTypesRadio_RadioButtonList_1"));

        IWebElement txtFromSuburb => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "From_SuburbText"));

        IWebElement txtToSuburb => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "From_SuburbText"));
        


    }
}
