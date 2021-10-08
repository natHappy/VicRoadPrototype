using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NewPOM.Base
{
    /// <summary>
    ///reference from https://github.com/anshooarora/extentreports-csharp/tree/master/ExtentReports/ExtentReports.Tests/Parallel
    /// </summary>
   
    public class ExtentReportContext
    {

        private static readonly Lazy<AventStack.ExtentReports.ExtentReports> _lazy = new Lazy<AventStack.ExtentReports.ExtentReports>(() => new AventStack.ExtentReports.ExtentReports());

        public static AventStack.ExtentReports.ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentReportContext()
        {
            string reportPath = System.IO.Directory.GetParent(@"../../../").FullName
                                 + Path.DirectorySeparatorChar + "Result"
                                   + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HH") + ".html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.ReportName = "ExtentReport" ;
            htmlReporter.Config.DocumentTitle = "Extent/NUnit Samples";
            htmlReporter.Config.Theme = Theme.Standard;

            Instance.AttachReporter(htmlReporter);
        }

        private ExtentReportContext()
        {
        }
    }
}
