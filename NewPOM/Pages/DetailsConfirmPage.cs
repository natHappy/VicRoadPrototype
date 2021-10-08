using NewPOM.Bases;
using NewPOM.Custom;
using NewPOM.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Pages
{
    class DetailsConfirmPage
    {
        private IWebDriver _driver;
        public DetailsConfirmPage(IWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement checkboxAgree => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "AgreeCheckBox_chkAgree"));
        IWebElement labelPermitFee => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "spanPermitFee"));
        IWebElement labelTacCharge => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "spanTacCharge"));



        public void SelectAggree()
        {
            checkboxAgree.SelectCheckbox("Y");
        }
        public void DeSelectAggree()
        {
            checkboxAgree.SelectCheckbox("N");
        }

        public bool CheckFeeDetails(string permitFee, string tacCharge)
        {
            if (labelPermitFee.GetText().Equals(permitFee)
                && labelTacCharge.GetText().Equals(tacCharge))
            {
                //LogUtil.Write("Fee Details are correct ");
                return true;
            }
            return false;
        }
    }
}
