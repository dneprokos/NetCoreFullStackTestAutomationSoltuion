using OpenQA.Selenium.Support.UI;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage
{
    public class BuildingsPage
    {
        private readonly BuildingsPageLocators _locators;

        public BuildingsPage()
        {
            _locators = new BuildingsPageLocators();
        }

        public BuildingsPage SelectFilterDropDownByText(string text)
        {
            var select = new SelectElement(_locators.SelectFilterBase);
            select.SelectByText(text, true);
            return this;
        }

    }
}
