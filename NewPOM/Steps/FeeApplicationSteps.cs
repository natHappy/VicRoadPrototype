using NewPOM.Bases;
using NewPOM.Config;
using NewPOM.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NewPOM.Steps
{
    [Binding]
    public class FeeApplicationSteps 
    {
        private IWebDriver _driver;
        FeeCalculatePage feeCalPage;
        SelectPermitTypePage selectPermit;
        VehicleDetailsPage vehicleDetails;
        ApplicationDetailsPage appDetails;
        DetailsConfirmPage confirmDetails;
        MakePaymentPage payment;

        public FeeApplicationSteps(IWebDriver driver)
        {
            _driver = driver;
            feeCalPage = new FeeCalculatePage(_driver);
            selectPermit = new SelectPermitTypePage(_driver);
            vehicleDetails = new VehicleDetailsPage(_driver);
            appDetails = new ApplicationDetailsPage(_driver);
            confirmDetails = new DetailsConfirmPage(_driver);
            payment = new MakePaymentPage(_driver);
        }
       

        [Given(@"I launch the CalculateFeePage")]
        public void GivenILaunchTheCalculateFeePage()
        {
            _driver.Navigate().GoToUrl(Settings.AUT);
        }
        
        [Given(@"I enter vehicleType, subType, address, startDate, duration")]
        public void GivenIEnterVehicleTypeSubTypeAddressStartDateDuration(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            feeCalPage.EnterFeeCalInfo(data.VehicleType, data.SubType, data.VehicleAddress, data.StartDate, data.Duration);
        }
        
        [Given(@"I click the Calculate button")]
        public void GivenIClickTheCalculateButton()
        {
            feeCalPage.CalculateFee();
            Thread.Sleep(1000);

        }
        [Then(@"the permitCost will displayed")]
        public void ThenThePermitCostWillDisplayed(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            Assert.That(feeCalPage.checkPermitCost(data.PermitCost), Is.True, "Wrong permit cost");
        }

        [When(@"I click Next button")]
        public void WhenIClickNextButton()
        {
            _driver.GoToNextPage();
        }

        [When(@"I enter Permit Type Details")]
        public void WhenIEnterPermitTypeDetails()
        {
            //doing nothing for now
            Thread.Sleep(1200);
            _driver.IsProgressBarTitleDisplayed("Step 2 of 7 : Select permit type");
        }

        [When(@"I enter Vehicle Details")]
        public void WhenIEnterVehicleDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
           vehicleDetails.EnterVehicleDetails(data.Make, data.Color, data.YearMade.ToString(), data.IdType, data.VehicleId, data.Agree);
        }

        [When(@"I enter Enter Individual Application Details")]
        public void WhenIEnterEnterIndividualApplicationDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            string phone = "0" + data.Phone.ToString();
            appDetails.EnterIndividualApplicationDetails(data.FirstName, data.LastName, data.ResidentialYes, phone, data.Email);
            Thread.Sleep(1200);
        }

        [Then(@"The permitFee and tacCharge will displayed")]
        public void ThenThePermitFeeAndTacChargeWillDisplayed(Table table)
        {
            Thread.Sleep(1500);
            dynamic data = table.CreateDynamicInstance();
            Assert.That(confirmDetails.CheckFeeDetails(data.PermitFee, data.TacCharge), Is.True, "Wrong permit cost detail");
        }

        [When(@"I select aggree checkbox")]
        public void WhenISelectAggreeCheckbox()
        {
            confirmDetails.SelectAggree();
            Thread.Sleep(700);
        }

        [Then(@"I can enter payment details")]
        public void ThenICanEnterPaymentDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            payment.EnterPaymentDetails(data.CardholderName, data.CardNumber.ToString(), data.Month, data.Year.ToString(), data.Cvn.ToString());
        }


        [Then(@"I can click the ProceedPayment button")]
        public void ThenICanClickTheProceedPaymentButton()
        {
            payment.MakePayment();
        }

    }
}
