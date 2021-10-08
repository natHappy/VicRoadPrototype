using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

[SetUpFixture]
class AssemblySetupTeardown
{
    public static AventStack.ExtentReports.ExtentReports _extent;
    public static ExtentTest _test;
    [OneTimeSetUp]
    public void AssemblySetup()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-AT");
    }

    [OneTimeTearDown]
    public void AssemblyTearDown()
    {
        _extent.Flush();
    }
}

namespace NewPOM.UnitTests
{
   
    [TestFixture]
    public class SetupFixture1
    {
        public static AventStack.ExtentReports.ExtentReports _extent;
        public static ExtentTest _test;

        [OneTimeSetUp]
        public void StartReport()
        {
            _extent = new AventStack.ExtentReports.ExtentReports();
            var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
            var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + ".html");
            _extent.AddSystemInfo("Environment", "DEV");
            _extent.AddSystemInfo("User Name", "Natasha");
            _extent.AttachReporter(htmlReporter);

            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void aftertest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    _test.Log(logstatus, "Test " + logstatus + " – " + errorMessage + stacktrace);
                    break;
                default:
                    logstatus = Status.Pass;
                    _test.Log(logstatus, "Test " + logstatus);
                    break;
            }
        }

         
    }
}
