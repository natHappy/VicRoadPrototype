using NewPOM.Bases;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewPOM.Custom
{
    public static class ElementCustomControl
    {

        public static void SelectDDLByText(this IWebElement element, string value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }

        public static void SelectDDLByValue(this IWebElement element, string value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByValue(value);
        }
        public static void SelectDDLByIndex(this IWebElement element, int value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByIndex(value);
        }


       

        public static string GetText(this IWebElement element)
        {
            return element.Text;
        }

        public static string GetLinkText(this IWebElement element)
        {
            return element.Text;
        }

        public static string GetSelectedDropDown(this IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().ToString();
        }


        public static IList<IWebElement> GetSelectedListOptions(this IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions;
        }

        public static void SelectDropDownList(this IWebElement element, string value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }
        /// <summary>
        /// Check if the element exist
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static bool IsElementPresent(this IWebElement element)
        {
            try
            {
                bool b = element.Displayed;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Assert if the Element is present
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
                throw new AssertionException(String.Format("AssertElementNotPresent exception"));
        }

        public static void SelectCheckbox(this IWebElement element, string value)
        {
            if ((element.Selected == false) && (value.ToUpper() == "Y")){
                element.Click();
            }
        }
        public static void DeSelectCheckbox(this IWebElement element, string value)
        {
            if ((element.Selected) && (value.ToUpper() == "N"))
            {
                element.Click();
            }
        }
    }
}
