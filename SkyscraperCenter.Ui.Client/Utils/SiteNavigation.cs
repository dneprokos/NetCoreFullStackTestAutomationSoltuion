using SeleniumBase.Client.WebDriverBase;

namespace SkyscraperCenter.Ui.Client.Utils
{
    public class SiteNavigation
    {
        public void GoToPage(string url)
        {
            WebDriverFactory.DriverContext.Navigate().GoToUrl(url);
        }
    }
}
