using TestsBase.Client.Managers;

namespace SkyscraperCenter.Ui.Client.Constants
{
    public class PageUrls
    {
        public static readonly string BaseUrl = TestSettingsManager.BaseUrl;

        public static readonly string TallestBuildingsPageUrl 
            = $"{BaseUrl}/buildings";
        public static readonly string TallestBuildingsWithFilterUnderConstructionPageUrl 
            = $"{BaseUrl}/buildings?list=tallest100-construction";
        public static readonly string TallestBuildingsWithFilterCompletedPageUrl
            = $"{BaseUrl}/buildings?list=tallest100-completed";

        public static readonly string CitiesPageUrl = $"{BaseUrl}/cities";
    }
}
