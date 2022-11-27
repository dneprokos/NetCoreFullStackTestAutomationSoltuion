using TestsBase.Client.Managers;

namespace SkyscraperCenter.Ui.Client.Constants
{
    public class PageUrls
    {
        public static readonly string BaseUrl = TestSettingsManager.BaseUrl;

        //Tallest Buildings page
        public static readonly string TallestBuildingsPageUrl 
            = $"{BaseUrl}/buildings";
        public static readonly string TallestBuildingsWithFilterUnderConstructionPageUrl 
            = $"{BaseUrl}/buildings?list=tallest100-construction";
        public static readonly string TallestBuildingsWithFilterCompletedPageUrl
            = $"{BaseUrl}/buildings?list=tallest100-completed";

        //Cities page
        public static readonly string CitiesPageUrl = $"{BaseUrl}/cities";

        //Height-Calculator page
        public static readonly string HeightCalculatorPageUrl
            = $"{BaseUrl}/height-calculator";
    }
}
