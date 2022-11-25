using FluentAssertions;
using NUnit.Framework;
using TestsBase.Client.Managers;

namespace JsonPlaceholder.Api.Tests.ApiTests
{
    [SetUpFixture]
    public class CommonTestFixtureSetup
    {
        [OneTimeSetUp]
        public void CommonTestsSetup()
        {
            TestSettingsManager.RestApiUrl.Should().NotBeNullOrEmpty("restApiBaseUrl is required to run the tests");
            TestSettingsManager.RestApiVersion.Should().NotBeNullOrEmpty("restApiVersion is required to run the tests");
        }
    }
}
