using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using WebScrapping.Models;

namespace WebScrapping.Services
{
    public class ChromeSeleniumService : IChromeSeleniumService
    {
        private readonly ChromeDriver _chromeDriver;
        public ChromeSeleniumService()
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable--gpu");

            _chromeDriver = new ChromeDriver(options);
        }

        public void GoToUrl(string url)
        {
            _chromeDriver?.Navigate().GoToUrl(url);
        }

        public IWebElement GetElementByCssSelector(string selector)
        {
            try
            {
                return _chromeDriver.
                    FindElement(By.CssSelector(selector));
            }
            catch (Exception)
            {
                return null;
            }

        }

        public string GetAttributeByByCssSelector(string selector, AttributeTypeEnum type)
        {
            IWebElement element = GetElementByCssSelector(selector);
            switch (type)
            {
                case AttributeTypeEnum.ImagePath:
                    return GetElementByAttribute(element, "src");
                case AttributeTypeEnum.Link:
                    return GetElementByAttribute(element, "href");
                case AttributeTypeEnum.Text:
                default:
                    return element?.Text;
            }
        }

        public ReadOnlyCollection<IWebElement> GetElementsByCssSelector(string selector)
        {
            return _chromeDriver
                .FindElements(By.CssSelector(selector));
        }

        public void ReturnToPreviousPage()
        {
            _chromeDriver.Navigate().Back();
        }

        public string GetElementByAttribute(IWebElement webElement, string attributeType)
        {
            return webElement?.GetAttribute(attributeType);
        }
        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(_chromeDriver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(100);
            }
        }

        public void CloseWindow()
        {
            Actions builder = new Actions(_chromeDriver);
            builder.MoveByOffset(0, 0).Click().Perform();

            //// get window handle
            //String baseWindowHdl = _chromeDriver.CurrentWindowHandle;

            //_chromeDriver.Close();

            //// switch back to base window
            //_chromeDriver.SwitchTo().Window(baseWindowHdl);

        }
        public void MoveAndClick(string cssSelector)
        {
            ((IJavaScriptExecutor)_chromeDriver).
                ExecuteScript("window.scrollTo(document.body.scrollHeight, document.body.scrollHeight - 150)");
        }
    }
}
