using NewPOM.Bases;
using NewPOM.Custom;
using NewPOM.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NewPOM.Pages
{
    class FeeCalculatePage
    {
        
        private IWebDriver _driver;
        public FeeCalculatePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement lstVehicleType => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "VehicleType_DDList"));
        public IWebElement lstPassengerVehicleSubType => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "PassengerVehicleSubType_DDList"));
        public IWebElement lstCarryingCapacity => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "GoodsVehicleSubType_DDList"));
        public IWebElement lstEngineCapacity => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "MotorcycleSubType_DDList"));
        public IWebElement lstPermitDuration => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "PermitDuration_DDList"));
        public IWebElement txtAddress => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "AddressLine_SingleLine_CtrlHolderDivShown"));
        public IWebElement btnStartDatePicker => _driver.FindElement(By.CssSelector("img[class='ui-datepicker-trigger']"));
        public IWebElement txtPermitEndDate => _driver.FindElement(By.XPath("//*[@id='PermitDurationModule']/span[2]"));
        public IWebElement btnFeeCalc => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "btnCal"));
        public IWebElement btnNext => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "btnNext"));

        public IWebElement labelPermitCost => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "spnTotalPermit"));
        public void EnterFeeCalInfo(string vehicleType, string subType, string address, string startDate, string duration)
        {
            lstVehicleType.SelectDDLByText(vehicleType); //
            EnterVehicleSubType(vehicleType, subType);
            txtAddress.SendKeys(address);
            //should handle clear all first before 
            btnStartDatePicker.Click();
            Thread.Sleep(1000);
            DatePicker.SelectDate(_driver, startDate);
            Thread.Sleep(500);
            //Permit duration List
            lstPermitDuration.SendKeys(duration);
            Thread.Sleep(1000);
        }

        ////Summary
        /// select Vehicle subtype, depending on previous vehicleType input
        /// /
        /// </summary>
        /// <param name="vehicleType"></param>
        /// <param name="subType"></param>
        private void EnterVehicleSubType(string vehicleType, string subType)
        {
            switch (vehicleType)
            {
                case ConstantValues.passengerVehicle:
                    lstPassengerVehicleSubType.SelectDDLByText(subType);
                    break;

                case ConstantValues.goodsCarryingVehicle:
                    lstCarryingCapacity.SelectDDLByText(subType);
                    break;
            }

        }

        public void CalculateFee()
        {
            btnFeeCalc.Click();
            Thread.Sleep(1500);
        }

        public bool checkPermitCost(string permitCost)
        {
            string cost = labelPermitCost.GetText();
            if (labelPermitCost.GetText().Equals(permitCost))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
