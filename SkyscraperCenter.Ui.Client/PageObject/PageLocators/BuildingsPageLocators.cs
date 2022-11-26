using OpenQA.Selenium;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;

namespace SkyscraperCenter.Ui.Client.PageObject.PageLocators
{
    public class BuildingsPageLocators : _LocatorsBase
    {
        public IWebElement SelectFilterBase =>
            WebDriverFactory.DriverContext.WaitForElement(By.CssSelector("select.select-lists-pages"), TimeOutSeconds);

    }
}
