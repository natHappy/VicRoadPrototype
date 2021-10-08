using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Bases
{
    public class ParallelConfig
    {
        public RemoteWebDriver Driver { get; set; }

        public BasePage CurrentPage { get; set; }
    }
}
