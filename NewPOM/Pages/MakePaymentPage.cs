using NewPOM.Bases;
using OpenQA.Selenium;
using NewPOM.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using NewPOM.Utils;
using System.Threading;

namespace NewPOM.Pages
{
    class MakePaymentPage
    {
        private IWebDriver _driver;
        public MakePaymentPage(IWebDriver driver)
        {
            _driver = driver;
            
        }
        IWebElement txtCardHolderName => _driver.FindElement(By.XPath("/html/body/div/form/div[1]/div/input"));
        IWebElement txtCardNumber => _driver.FindElement(By.Id("cardNumber"));
        IWebElement lstExpiredMonth => _driver.FindElement(By.Id("expiryDateMonth"));
        IWebElement lstExpiredYear => _driver.FindElement(By.Id("expiryDateYear"));
        IWebElement txtCvn => _driver.FindElement(By.Id("cvn")); //text
        IWebElement labelPermitFee => _driver.FindElement(By.XPath("//*[@id='" + ConstantValues.locatorPrefix + "paymentPanel_orderSummary_pnlOrderSummary']/table/tbody/tr[2]/td[2]")); // check subtotal
        IWebElement labelSurcharge => _driver.FindElement(By.XPath("//*[@id='" + ConstantValues.locatorPrefix + "paymentPanel_orderSummary_divSurcharge']/table/tbody/tr[2]/td[2]/span[1]")); // check card fee
        IWebElement labelTotal => _driver.FindElement(By.XPath("//*[@id='" + ConstantValues.locatorPrefix + "paymentPanel_orderSummary_tdStandardTotals']/span[2]")); //check totall paymnet

        IWebElement btnPayment => _driver.FindElement(By.Id(ConstantValues.locatorPrefix + "paymentPanel_btnNext"));
        public void EnterPaymentDetails(string cardholderName, string cardNumber, int month,string year, string cvn)
        {
            Thread.Sleep(1000); //dirty wait for now, just incase Chrome is too fast, page not fully loaded
            _driver.SwitchTo().Frame(0);
            Thread.Sleep(2000); //dirty wait for now, just incase Chrome is too fast, page not fully loaded
            txtCardHolderName.SendKeys(cardholderName);
            txtCardNumber.SendKeys(cardNumber);
            lstExpiredMonth.SelectDDLByIndex(month);
            lstExpiredYear.SelectDDLByValue(year);
            txtCvn.SendKeys(cvn); //text
        }
        public void MakePayment()
        {
            _driver.SwitchTo().ParentFrame();
            btnPayment.Click();
        }

        public bool IsInvalidCreditCard()
        {
            return true;
        }
    }
}
