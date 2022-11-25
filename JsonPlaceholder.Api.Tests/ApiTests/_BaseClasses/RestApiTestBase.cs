using NUnit.Allure.Core;
using NUnit.Framework;

namespace JsonPlaceholder.Api.Tests.ApiTests._BaseClasses
{
    [AllureNUnit]
    public abstract class RestApiTestBase
    {
        [OneTimeSetUp]
        public void BeforeFeature()
        {
            //TODO: Add some basic before feature logic
        }

        [OneTimeSetUp]
        public void AfterFeature()
        {
            //TODO: Add some basic after feature logic
        }

        [SetUp]
        public void BeforeTest()
        {
            //TODO: Add some basic before test logic
        }

        [TearDown]
        public void AfterTest()
        {
            //TODO: Add some basic after test logic
        }
    }
}
