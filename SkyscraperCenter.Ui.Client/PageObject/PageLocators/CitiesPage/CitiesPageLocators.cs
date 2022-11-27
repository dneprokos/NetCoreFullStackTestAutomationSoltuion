using OpenQA.Selenium;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;

namespace SkyscraperCenter.Ui.Client.PageObject.PageLocators.CitiesPage
{
    public class CitiesPageLocators : _LocatorsBase
    {
        public IWebElement SelectFilterBaseElement =>
            WebDriverFactory.DriverContext.WaitForElement(By.CssSelector("select.select-lists-pages"), TimeOutSeconds);
    }
}
