using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SkyscraperCenter.Ui.Client.Enums;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.BuildingPage;
using SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage.PageComponents;
using TestsBase.Client.Extensions;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage
{
    public class BuildingsPage
    {
        public BuildingsPageTable BuildingsTable;

        private readonly BuildingsPageLocators _locators;

        public BuildingsPage()
        {
            _locators = new BuildingsPageLocators();
            BuildingsTable = new BuildingsPageTable();
        }

        public BuildingsPage SelectFilterDropDownByText(string text, bool verifyIfApplied = false)
        {
            var select = new SelectElement(_locators.SelectFilterBaseElement);
            select.SelectByText(text, true);

            if (verifyIfApplied)
            {
                new SelectElement(_locators.SelectFilterBaseElement).SelectedOption.Text.Should().Contain(text);
            }

            return this;
        }

        public BuildingsPage SelectFilterDropDownByText(BuildingsFilterOptions text, bool verifyIfApplied = false) 
            => SelectFilterDropDownByText(text.GetDescription(), verifyIfApplied);
    }
}
