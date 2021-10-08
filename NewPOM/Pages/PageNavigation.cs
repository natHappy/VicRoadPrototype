using NewPOM.Bases;
using NewPOM.Custom;
using NewPOM.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NewPOM.Pages
{
    public static class PageNavigation
    {
        public static bool IsProgressBarTitleDisplayed(this IWebDriver driver, string title)
        {
            IWebElement lableTitle = driver.FindElement(By.CssSelector("p[class='progress-bar-title']"));
            if (lableTitle.GetText().Equals(title))
            {
                return true;
            }
            return false;
        }
        public static void GoToNextPage(this IWebDriver driver)
        {
            try
            {
                IWebElement btnNext = driver.FindElement(By.Id(ConstantValues.locatorPrefix + "btnNext"));
                btnNext.Click();
                //wait 0.5 sec for next page fully load
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                LogUtil.Write("ERROR:: " + e.StackTrace);
            }
        }

        public static void GoBackPrevPage(this IWebDriver driver)
        {
            try
            {
                IWebElement btnBack = driver.FindElement(By.Id(ConstantValues.locatorPrefix + "btnBack"));
                btnBack.Click();
                //wait 0.5 sec for prev page fully load
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                LogUtil.Write("ERROR:: " + e.StackTrace);
            }
        }
    }
}
