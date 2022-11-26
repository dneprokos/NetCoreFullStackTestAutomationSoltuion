using FluentAssertions;
using TestsBase.Client.Managers;

namespace TestsBase.Client.Utils
{
    public static class ConfigurationHelper
    {
        private const string MessageEnding = "Please verify *.runsettings file contains this value";

        public static void VerifySettingsRequiredForSeleniumTests()
        {
            TestSettingsManager.Browser.Should().NotBeNullOrEmpty($"browser is required for WebDriver initialization. {MessageEnding}");
            TestSettingsManager.BaseUrl.Should().NotBeNullOrEmpty($"baseUrl is required. {MessageEnding}");
            if (TestSettingsManager.IsRemoteDriver)
            {
                TestSettingsManager.SeleniumHubUri.Should()
                    .NotBeNull($"seleniumHubUrl is required when you want to run remote driver. {MessageEnding}");
            }
        }

        public static void VerifySettingsRequiredForRestApiTests()
        {
            TestSettingsManager.RestApiUrl.Should().NotBeNullOrEmpty($"restApiBaseUrl is required to run the tests. {MessageEnding}");
            TestSettingsManager.RestApiVersion.Should().NotBeNullOrEmpty($"restApiVersion is required to run the tests. {MessageEnding}");
        }
    }
}
