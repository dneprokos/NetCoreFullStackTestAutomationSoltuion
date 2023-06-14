using OpenQA.Selenium;
using SeleniumBase.Client.Utils;

namespace SeleniumBase.Client.WebElements
{
    public class ReadonlyTextWebelement : BaseWebelement
    {
        public ReadonlyTextWebelement(IWebElement webElement) : base(webElement)
        {
        }

        public string GetText() => _element.Text;

        public string GetInnerText() 
            => new JsHelper()
            .RunJavaScript("return arguments[0].innerText; ", _element) as string;
    }
}
