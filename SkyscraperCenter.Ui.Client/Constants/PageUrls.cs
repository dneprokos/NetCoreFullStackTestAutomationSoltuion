using TestsBase.Client.Managers;

namespace SkyscraperCenter.Ui.Client.Constants
{
    public class PageUrls
    {
        public static readonly string BaseUrl = TestSettingsManager.BaseUrl;

        public static readonly string TallestBuildingsPageUrl = $"{BaseUrl}/buildings";
        public static readonly string CitiesPageUrl = $"{BaseUrl}/cities";
    }
}
