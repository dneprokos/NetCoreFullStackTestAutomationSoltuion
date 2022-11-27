using SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage;
using SkyscraperCenter.Ui.Client.PageObject.Pages.CitiesPage;
using SkyscraperCenter.Ui.Client.PageObject.Pages.HeightCalculatorPage;

namespace SkyscraperCenter.Ui.Client.Facades
{
    public class PageObjects
    {
        public BuildingsPage BuildingsPage => new BuildingsPage();
        public CitiesPage CitiesPage => new CitiesPage();
        public HeightCalculatorPage HeightCalculatorPage => new HeightCalculatorPage();
    }
}
