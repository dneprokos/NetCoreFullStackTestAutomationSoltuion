using OpenQA.Selenium;

namespace SeleniumBase.Client.WebElements
{
    public class InputFieldWebelement : ReadonlyTextWebelement
    {
        public InputFieldWebelement(IWebElement webElement) : base(webElement)
        {
        }

        public void SetValue(string value)
        {
            _element.Clear();
            _element.SendKeys(value);
        }

        public void Submit()
        {
            _element.Submit();
        }
    }
}
