using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SkyscraperCenter.Ui.Client.Enums;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.CitiesPage;
using SkyscraperCenter.Ui.Client.PageObject.Pages.CitiesPage.Components;
using TestsBase.Client.Extensions;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.CitiesPage
{
    public class CitiesPage
    {
        public CitiesPageTable Table;

        private readonly CitiesPageLocators _locators;

        public CitiesPage()
        {
            _locators = new CitiesPageLocators();
            Table = new CitiesPageTable();
        }

        public CitiesPage SelectFilterDropDownByText(string text, bool verifyIfApplied = false)
        {
            var select = new SelectElement(_locators.SelectFilterBaseElement);
            select.SelectByText(text, true);

            if (verifyIfApplied)
            {
                new SelectElement(_locators.SelectFilterBaseElement).SelectedOption.Text.Should().Contain(text);
            }

            return this;
        }

        public CitiesPage SelectFilterDropDownByText(CitiesFilterOptions text, bool verifyIfApplied = false)
        {
            return SelectFilterDropDownByText(text.GetDescription(), verifyIfApplied);
        }
    }
}
