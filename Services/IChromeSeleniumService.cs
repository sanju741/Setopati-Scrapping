using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebScrapping.Models;

namespace WebScrapping.Services
{
    public interface IChromeSeleniumService
    {
        void GoToUrl(string url);
        IWebElement GetElementByCssSelector(string selector);
        string GetAttributeByByCssSelector(string selector, AttributeTypeEnum type);
        ReadOnlyCollection<IWebElement> GetElementsByCssSelector(string selector);
        void ReturnToPreviousPage();
        string GetElementByAttribute(IWebElement webElement, string attributeType);
        void WaitForAjax();
        void CloseWindow();
        void MoveAndClick(string cssSelector);
    }
}