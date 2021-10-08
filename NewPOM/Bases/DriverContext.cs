using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPOM.Bases
{
    public class DriverContext
    {
        public IWebDriver Driver { get; set; }
        public Browser Browser { get; set; }
    }
}
