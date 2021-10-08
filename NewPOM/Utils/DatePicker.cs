using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using NewPOM.Utils;
using System.Text;

using System.Linq;

namespace NewPOM.Utils
{
    
    public static class DatePicker
    {
        public static void SelectDate(IWebDriver driver, string date)
        {
            string[] separator = { "/" };
            string[] dateEle = date.Split("/");

           /* cannot find - also for cssSelector - need follow up
           IWebElement datePickerTable = _driver.FindElement(By.XPath("//table[@class='ui-datepicker-calendar']"));
           IWebElement nextMonth = _driver.FindElement(By.XPath("​//*[@id='ui-datepicker-div']/descendant::span[@class='ui-icon ui-icon-circle-triangle-e']"));
           */

            //IWebElement prevMonth = driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div/a[1]/span"));
            //IWebElement nextMonth = driver.FindElement(By.XPath("​//*[@id='ui-datepicker-div']/descendant::span[@class='ui-icon ui-icon-circle-triangle-e']"));
            //IWebElement nextMonth = driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div/a[2]/span"));
            // this is for select previous month -
            //will need to implement another routine to work out proper month year before selecting date
            //builder.Click(prevMonth).Build().Perform();

            IReadOnlyCollection< IWebElement> days = driver.FindElements(By.XPath("//a[@class='ui-state-default']")); //doesn't like CssSelector - dont know why

            Actions builder = new Actions(driver);

            DateTime today = DateTime.Today;
            int todayDate = today.Day;

            IWebElement day = (IWebElement)days.ElementAt(dateEle[0].ParseInt() - todayDate - 1);
            builder.Click(day).Build().Perform();

        }

    }
}
