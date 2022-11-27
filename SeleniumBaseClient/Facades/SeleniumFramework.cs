using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;

namespace SeleniumBase.Client.Facades
{
    public class SeleniumFramework
    {
        public static WebDriverFactory WebDriverFactory => new WebDriverFactory();

        public static JsHelper JavaScriptHelper => new JsHelper();
    }
}
