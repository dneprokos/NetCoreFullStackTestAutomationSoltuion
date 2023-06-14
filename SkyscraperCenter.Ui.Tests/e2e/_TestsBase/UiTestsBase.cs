using NUnit.Framework;
using SeleniumBase.Client.Facades;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;
using TestsBase.Client.Managers;

namespace SkyscraperCenter.Ui.Tests.e2e._TestsBase
{
    public abstract class UiTestsBase
    {
        [SetUp]
        public void BeforeTest()
        {
            var driverOptions = new WebDriverCapabilities()
            {
                Browser = TestSettingsManager.Browser,
                IsHeadless = TestSettingsManager.IsHeadlessMode,
                IsRemote = TestSettingsManager.IsRemoteDriver,
                SeleniumHubUri = TestSettingsManager.SeleniumHubUri,
                WindowSize = TestSettingsManager.WindowSize
            };

            SeleniumFramework.WebDriverFactory.StartDriver(driverOptions);
        }

        [TearDown]
        public void TearDown()
        {
            ScreenShotManager.MakeScreenOnTestFail();
            SeleniumFramework.WebDriverFactory.StopDriver();
        }
    }
}
