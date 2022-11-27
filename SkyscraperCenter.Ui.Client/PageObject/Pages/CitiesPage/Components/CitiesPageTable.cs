using System.Collections.Generic;
using SeleniumBase.Client.Utils.ComponentBasics;
using SkyscraperCenter.Ui.Client.PageObject.PageLocators.CitiesPage.Components;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.CitiesPage.Components
{
    public class CitiesPageTable : TableBasics
    {
        private CitiesPageTableLocators _locators;

        public CitiesPageTable()
        {
            _locators = new CitiesPageTableLocators();
        }
    }
}
