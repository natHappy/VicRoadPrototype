using NewPOM.Bases;
using NewPOM.Custom;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Pages
{
    class VehicleDetailsPage
    {
        private IWebDriver _driver;
        public VehicleDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        IWebElement txtMake => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "Make_SingleLine_CtrlHolderDivShown"));
        IWebElement lstColor => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "Colour_DDList"));
        IWebElement txtYear => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "YearOfManufacture_txtYearOfManufacture"));
        IWebElement lstVehicleIdentification => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "VehicleIdentificationType_DDList"));
        IWebElement txtVehicleIdentificationNumber => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "VehicleIdentificationNumber_PaymentSingleLine"));
        IWebElement txtChassisNumber => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "ChassisNumber_PaymentSingleLine"));
        IWebElement txtEngineNumber => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "EngineNumber_PaymentSingleLine"));
        IWebElement checkboxAgree => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "AgreeCheckBox_chkAgree"));

        public void EnterVehicleDetails(string make, string color, string yearMade, string idType, string vehicleId, string aggree)
        {

            txtMake.SendKeys(make);
            lstColor.SelectDDLByValue(color);
            txtYear.SendKeys(yearMade);
            lstVehicleIdentification.SelectDDLByValue(idType);
            EnterVehicleID(idType, vehicleId);
            checkboxAgree.SelectCheckbox(aggree);
        }

        public void EnterVehicleID(string idType, string vehicleId) {
            switch (idType) {
                case "Vehicle Identification Number (VIN)":
                    txtVehicleIdentificationNumber.SendKeys(vehicleId);
                    break;
                case "Engine number":
                    txtEngineNumber.SendKeys(vehicleId);
                    break;
                case "Chassis number":
                    txtChassisNumber.SendKeys(vehicleId);
                    break;
            }
        }

    }
}
