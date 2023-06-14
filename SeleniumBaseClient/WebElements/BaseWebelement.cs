using OpenQA.Selenium;

namespace SeleniumBase.Client.WebElements
{
    public class BaseWebelement
    {
        protected readonly IWebElement _element;

        public BaseWebelement(IWebElement webElement)
        {
            _element= webElement;
        }

        public bool IsDisplayed() => _element.Displayed; 
    }
}