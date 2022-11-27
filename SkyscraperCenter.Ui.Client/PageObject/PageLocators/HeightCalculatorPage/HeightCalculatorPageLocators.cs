using OpenQA.Selenium;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;

namespace SkyscraperCenter.Ui.Client.PageObject.PageLocators.HeightCalculatorPage
{
    public class HeightCalculatorPageLocators : _LocatorsBase
    {
        public IWebElement FloorsAboveGroundInputField => WebDriverFactory
            .DriverContext
            .WaitForElement(By.Id("floor-count"), TimeOutSeconds);

        //New Feature. TODO: Add wait helper extension method with relative by parameter
        public IWebElement HeightInputFieldRelativePath => WebDriverFactory
            .DriverContext.FindElement(
            RelativeBy.WithLocator(By.TagName("input")).RightOf(FloorsAboveGroundInputField));

        public IWebElement HeightInputField 
            => WebDriverFactory.DriverContext.WaitForElement(By.Id("height"), TimeOutSeconds);
    }
}
