using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.BuildingPage;
using SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage.PageComponents;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage
{
    public class BuildingsPage
    {
        public BuildingsTableComponent BuildingsTable;

        private readonly BuildingsPageLocators _locators;

        public BuildingsPage()
        {
            _locators = new BuildingsPageLocators();
            BuildingsTable = new BuildingsTableComponent(_locators);
        }

        public BuildingsPage SelectFilterDropDownByText(string text, bool verifyIfApplied = false)
        {
            var select = new SelectElement(_locators.SelectFilterBase);
            select.SelectByText(text, true);

            if (verifyIfApplied)
            {
                new SelectElement(_locators.SelectFilterBase).SelectedOption.Text.Should().Contain(text);
            }

            return this;
        }
    }
}
