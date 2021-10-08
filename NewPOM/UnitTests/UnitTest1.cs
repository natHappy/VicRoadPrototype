using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using MySql.Data.MySqlClient;
using NewPOM.Base;
using NewPOM.Bases;
using NewPOM.Config;
using NewPOM.Custom;
using NewPOM.Pages;
using NewPOM.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NewPOM.UnitTests
{
    [TestFixture]
    class UnitTest1: BaseFixture
    {
        private DriverContext _driverContext = new DriverContext();

        string[] feeCalInput = new String[5];
        string[] vehicleDetailsInput = new String[6];

        [SetUp]
        public void Setup()
        {
            InitializeSettings();
            NaviateSite();
            LogUtil.Write("test setup");
        }

        [Test]
        public void Test1()
        {
            try
            {
                var test = ExtentTestCustom.CreateTest("EnterFeeCalInfo()");
                FeeCalculatePage feeCalPage = new FeeCalculatePage(_driverContext.Driver);
                feeCalPage.EnterFeeCalInfo("Passenger vehicle", "Sedan", "23 Loris St, SPRINGVALE SOUTH VIC 3172", "25/10/2021", "8 days");
                Thread.Sleep(1000);
                test.Pass("");

                test = ExtentTestCustom.CreateTest("CalculateFee()");
                feeCalPage.CalculateFee();
                Thread.Sleep(1000);
                Assert.That(feeCalPage.checkPermitCost("$56.40"), Is.True, "Wrong permit cost");
                test.Pass("CalculateFee()");

                test = ExtentTestCustom.CreateTest("GoToNextPage()");
                _driverContext.Driver.GoToNextPage();
                test.Pass("");

                //comment temporary as cannot even manually select "move car to" option - not sure why
                //SelectPermitTypePage selectPermit = new SelectPermitTypePage(DriverContext.Driver);
                test = ExtentTestCustom.CreateTest("GoToNextPage()");
                _driverContext.Driver.GoToNextPage();

                test = ExtentTestCustom.CreateTest("getDataInputFromExcel()");
                getDataInputFromExcel();
                test.Pass("");

                test = ExtentTestCustom.CreateTest("EnterVehicleDetails()");
                VehicleDetailsPage vehicleDetails = new VehicleDetailsPage(_driverContext.Driver);
                vehicleDetails.EnterVehicleDetails(vehicleDetailsInput[0], vehicleDetailsInput[1], vehicleDetailsInput[2], vehicleDetailsInput[3], vehicleDetailsInput[4], vehicleDetailsInput[5]);
                test.Pass("");
                test = ExtentTestCustom.CreateTest("GoToNextPage()");
                _driverContext.Driver.GoToNextPage();
                test.Pass("");

                test = ExtentTestCustom.CreateTest("EnterIndividualApplicationDetails()");
                ApplicationDetailsPage appDetails = new ApplicationDetailsPage(_driverContext.Driver);
                appDetails.EnterIndividualApplicationDetails("Natasha", "Nguyen", "Y", "0421467325", "quynhnhuhonguyen@gmail.com");
                test.Pass("");
                test = ExtentTestCustom.CreateTest("GoToNextPage()");
                _driverContext.Driver.GoToNextPage();
                test.Pass("");

                test = ExtentTestCustom.CreateTest("SelectAggree()");
                DetailsConfirmPage confirmDetails = new DetailsConfirmPage(_driverContext.Driver);
                Assert.That(confirmDetails.CheckFeeDetails("$25.60", "$30.80"), Is.True, "Wrong permit cost detail");
                confirmDetails.SelectAggree();
                test.Pass("");
                test = ExtentTestCustom.CreateTest("GoToNextPage()");
                _driverContext.Driver.GoToNextPage();
                test.Pass("");
                /*
                step = "EnterPaymentDetails()";
                MakePaymentPage payment = new MakePaymentPage(_driverContext);
                payment.EnterPaymentDetails("Natasha Nguyen", "12456789012", 7, "2027", "123");
                step = "MakePayment()";
                payment.MakePayment();
                _test.Pass(step);
                */
            }
            catch (Exception e)
            {
                var screenshot = ((ITakesScreenshot)_driverContext).GetScreenshot().AsBase64EncodedString;
                MediaEntityModelProvider mediaEntity = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, "").Build();
                ExtentTestCustom.GetTest().Fail(e.StackTrace + "\n\n", mediaEntity);
                Assert.Fail();
            }

        }
        [TearDown]
        public void TearDown()
        {
            if (_driverContext.Driver != null)
            {
                _driverContext.Driver.Quit();
            }
        }

        public void getDataInputFromExcel()
        {
            //get feeCalInput
            List<Datacollection>  table = conn.GetData("SELECT * FROM permit");
            feeCalInput[0] = MySqlUtil.ReadData(1, "VehicleType");
            feeCalInput[1] = MySqlUtil.ReadData(1, "VehicleSubType");
            feeCalInput[2] = MySqlUtil.ReadData(1, "VehicleAddress");
            feeCalInput[3] = MySqlUtil.ReadData(1, "StartDate");
            feeCalInput[4] = MySqlUtil.ReadData(1, "Duration");


            //get vehicleDetailsInput
            string fileName = Environment.CurrentDirectory.ToString() + "\\Data\\VehicleDetailsInput.xlsx";
            ExcelUtil.PopulateInCollection(fileName);
            vehicleDetailsInput[0] = ExcelUtil.ReadData(1, "Make");
            vehicleDetailsInput[1] = ExcelUtil.ReadData(1, "Color");
            vehicleDetailsInput[2] = ExcelUtil.ReadData(1, "YearMade");
            vehicleDetailsInput[3] = ExcelUtil.ReadData(1, "IdType");
            vehicleDetailsInput[4] = ExcelUtil.ReadData(1, "VehicleId");
            vehicleDetailsInput[5] = ExcelUtil.ReadData(1, "Agree");
        }

        void InitializeSettings()
        {
            //Set all the settings from GlobalConfig.xml for framework
            ConfigReader.SetFrameworkSettings();
            //Set Log
            LogUtil.CreateLogFile();
            //Open Browser
            OpenBrowser(Settings.BrowserType);
            LogUtil.Write("Initialised");
        }

        private void OpenBrowser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.FireFox:
                    _driverContext.Driver = new FirefoxDriver();
                    _driverContext.Browser = new Browser(_driverContext.Driver);
                    break;
                case BrowserType.Chrome:
                    ChromeOptions option = new ChromeOptions();
                    option.AddArguments("start-maximized");
                    option.AddArguments("--disable-gpu");
                    //option.AddArguments("--headless");
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driverContext.Driver = new ChromeDriver(option);
                    break;
            }
            _driverContext.Browser = new Browser(_driverContext.Driver);
            _driverContext.Browser.Type = Settings.BrowserType;
            _driverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);


        }

        public void NaviateSite()
        {
            _driverContext.Browser.GoToUrl(Settings.AUT);
            LogUtil.Write("Opened the browser !!!");
        }
    }
}
