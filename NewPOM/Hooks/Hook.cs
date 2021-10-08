using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using NewPOM.Bases;
using NewPOM.Config;
using NewPOM.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NewPOM.Hooks
{
    [Binding]
    public class Hook 
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private static ExtentHtmlReporter _htmlReporter;
        private static AventStack.ExtentReports.ExtentReports extent;
        private ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        
        public Hook(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            //Set Log - no need due to having extend report - not good for parallel execution
            //LogUtil.CreateLogFile();
            //LogUtil.Write("start hooking test");
        }


        //private static ExtentReports extent;
        //static string reportPath = System.IO.Directory.GetParent(@"../../../").FullName
        static string reportPath = Settings.ReportPath + Path.DirectorySeparatorChar + "Result"
                + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HH");

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Set all the settings from GlobalConfig.xml for framework
            ConfigReader.SetFrameworkSettings();

            //init report
            _htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(_htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            
            if (featureContext != null)
            {
                _feature = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Description);
            }
        }


        [BeforeScenario]
        public void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if (scenarioContext != null)
            {
                _scenarioContext = scenarioContext;
                //this is ScenarioContext
                _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title,scenarioContext.ScenarioInfo.Description);
            }
            //open browser
            OpenBrowser(Settings.BrowserType);
            //LogUtil.Write("After opening browser");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
            //LogUtil.CloseLog();
        }


        [AfterStep]
        public void RecordReportingSteps()
        {
            //logic -> Given when or then
            // add the node
            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;
            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    CreateNode<Then>();
                    break;
                default:
                    CreateNode<And>();
                    break;
            }
        }

        public void CreateNode<T>() where T: IGherkinFormatterModel
        {
            if (_scenarioContext.TestError == null)
            {
                _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
            }
            else
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
                MediaEntityModelProvider mediaEntity = MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, _scenarioContext.ScenarioInfo.Title.Trim()).Build();
                _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
            }
        }

        private void OpenBrowser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.FireFox:
                    _driver = new FirefoxDriver();
                    //Browser = new Browser(Driver);
                    break;
                case BrowserType.Chrome:
                    ChromeOptions option = new ChromeOptions();
                    option.AddArguments("start-maximized");
                    option.AddArguments("--disable-gpu");
                    //option.AddArguments("--headless");
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driver = new ChromeDriver(option);
                    break;
            }
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            //Browser = new Browser(Driver);
            //Browser.Type = Settings.BrowserType;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
        }

    }
}
