using NUnit.Framework;
using SeleniumBase.Client.Facades;
using TestsBase.Client.Utils;

namespace SkyscraperCenter.Ui.Tests.e2e
{
    [SetUpFixture]
    public class FixtureSetup
    {
        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            ConfigurationHelper.VerifySettingsRequiredForSeleniumTests();
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            SeleniumFramework.WebDriverFactory.QuitAndKillBrowserProcesses();
        }
    }
}
