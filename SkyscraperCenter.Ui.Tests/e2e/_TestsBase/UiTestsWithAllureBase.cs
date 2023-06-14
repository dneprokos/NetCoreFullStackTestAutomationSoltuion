using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace SkyscraperCenter.Ui.Tests.e2e._TestsBase
{
    [AllureNUnit]
    public abstract class UiTestsWithAllureBase : UiTestsBase
    {
        [SetUp]
        public new void BeforeTest()
        {
            base.BeforeTest();
            Console.WriteLine("Running tests with Allure reporting");
        }
    }
}
