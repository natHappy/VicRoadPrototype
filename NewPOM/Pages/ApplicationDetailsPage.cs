using NewPOM.Bases;
using NewPOM.Custom;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Pages
{
    class ApplicationDetailsPage
    {
        private IWebDriver _driver;
        public ApplicationDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement lstPermitCategory => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "PermitCategory_DDList"));
        IWebElement txtIndFirstName => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "IndFirstName_TxtName"));
        IWebElement txtIndLastName => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "IndLastName_TxtName"));
        IWebElement checkboxResidentialAddressYes => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "residential_address_yes")); //checkbox
        IWebElement txtIndPhone => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "IndPhoneNumber_TelephoneNumber"));
        IWebElement txtIndEmail => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "IndEmailId_EmailAddress"));
        
        public void EnterIndividualApplicationDetails(string firstName, string lastName, string residentialYes, string phone, string email)
        {
            txtIndFirstName.SendKeys(firstName);
            txtIndLastName.SendKeys(lastName); ;
            checkboxResidentialAddressYes.SelectCheckbox(residentialYes);
            txtIndPhone.SendKeys(phone);
            txtIndEmail.SendKeys(email);
        }

    }
}
