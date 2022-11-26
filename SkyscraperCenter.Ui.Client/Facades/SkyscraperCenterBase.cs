using SkyscraperCenter.Ui.Client.Utils;

namespace SkyscraperCenter.Ui.Client.Facades
{
    public class SkyscraperCenterBase
    {
        public static SiteNavigation QuickNavigation => new SiteNavigation();
        public static PageObjects PageObjects => new PageObjects();
    }
}
