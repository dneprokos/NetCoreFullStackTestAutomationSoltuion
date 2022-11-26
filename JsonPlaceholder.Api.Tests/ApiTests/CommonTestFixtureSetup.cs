using NUnit.Framework;
using TestsBase.Client.Utils;

namespace JsonPlaceholder.Api.Tests.ApiTests
{
    [SetUpFixture]
    public class CommonTestFixtureSetup
    {
        [OneTimeSetUp]
        public void CommonTestsSetup()
        {
            ConfigurationHelper.VerifySettingsRequiredForRestApiTests();
        }
    }
}
