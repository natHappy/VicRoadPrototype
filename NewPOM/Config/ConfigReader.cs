using NewPOM.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.XPath;

namespace NewPOM.Config
{
    class ConfigReader
    {
        public static void SetFrameworkSettings()
        {

            XPathItem aut;
            XPathItem browser;
            XPathItem testtype;
            XPathItem islog;
            XPathItem isreport;
            XPathItem buildname;
            XPathItem logPath;
            XPathItem reportPath;
            XPathItem appConnection;

            string strFilename = Environment.CurrentDirectory.ToString() + "\\Config\\GlobalConfig.xml";
            FileStream stream = new FileStream(strFilename, FileMode.Open);
            XPathDocument document = new XPathDocument(stream);
            XPathNavigator navigator = document.CreateNavigator();

            //Get XML Details and pass it in XPathItem type variables
            aut = navigator.SelectSingleNode("NewPOM/Settings/AUT");
            browser = navigator.SelectSingleNode("NewPOM/Settings/Browser");
            buildname = navigator.SelectSingleNode("NewPOM/Settings/BuildName");
            testtype = navigator.SelectSingleNode("NewPOM/Settings/TestType");
            islog = navigator.SelectSingleNode("NewPOM/Settings/IsLog");
            isreport = navigator.SelectSingleNode("NewPOM/Settings/IsReport");
            logPath = navigator.SelectSingleNode("NewPOM/Settings/LogPath");
            reportPath = navigator.SelectSingleNode("NewPOM/Settings/ReportPath");
            appConnection = navigator.SelectSingleNode("NewPOM/Settings/ApplicationDb");

            //Set XML Details in the property to be used accross framework
            Settings.AUT = aut.Value.ToString();
            Settings.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser.Value.ToString());
            Settings.BuildName = buildname.Value.ToString();
            Settings.TestType = testtype.Value.ToString();
            Settings.IsLog = islog.Value.ToString();
            Settings.IsReporting = isreport.Value.ToString();
            Settings.LogPath = logPath.Value.ToString();
            Settings.ReportPath = reportPath.Value.ToString();
            Settings.AppConnectionString = appConnection.Value.ToString();
        }

    }
}
