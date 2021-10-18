using AventStack.ExtentReports;
using MySql.Data.MySqlClient;
using NewPOM.Custom;
using NewPOM.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Base
{
    /// <summary>
    ///reference from https://github.com/anshooarora/extentreports-csharp/tree/master/ExtentReports/ExtentReports.Tests/Parallel
    /// </summary>
    /// 
    
    public class BaseFixture
    {
        //public static MySqlConnection conn { set; get; }

        [OneTimeSetUp]
        public void TestSuiteSetup()
        {
            //conn = MySqlUtil.DBConnect();
            ExtentTestCustom.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        protected void TestSuiteTearDown()
        {
            ExtentReportContext.Instance.Flush();
            //conn.DBClose();
        }

        [SetUp]
        public void BeforeTest()
        {
            ExtentTestCustom.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentTestCustom.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
        }
    }
}
