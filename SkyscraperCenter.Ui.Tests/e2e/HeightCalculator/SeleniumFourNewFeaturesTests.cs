using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;
using SkyscraperCenter.Ui.Client.Constants;
using SkyscraperCenter.Ui.Client.Facades;
using SkyscraperCenter.Ui.Tests.e2e._TestsBase;

namespace SkyscraperCenter.Ui.Tests.e2e.HeightCalculator
{
    [Parallelizable(ParallelScope.All)]
    [AllureSuite("HeightCalculator")]
    [AllureSubSuite("Testing of the new Selenium Four feature")]
    public class SeleniumFourNewFeaturesTests : UiTestsBase
    {
        [SetUp]
        public void BeforeEachTest()
        {
            SkyscraperCenterBase.QuickNavigation.GoToPage(PageUrls.HeightCalculatorPageUrl);
        }

        [Test]
        public void RelativePathTest()
        {
            //Arrange
            const string text = "Wakanda";

            //Act
            var page = SkyscraperCenterBase.Pages.HeightCalculatorPage;
            page.EnterTextToHeightInputField(text);

            //Assert
            page.ReadHeightInputTextValue().Should().Be(text);
        }
        
        [Test]
        public void NewTabTest()
        {
            //Arrange
            string originalTab = WebDriverFactory.DriverContext.CurrentWindowHandle;

            //Act
            WebDriverFactory.DriverContext.OpenNewTabOrWindow(WindowType.Tab);
            string newTab = WebDriverFactory.DriverContext.CurrentWindowHandle;

            //Assert
            using (new AssertionScope())
            {
                newTab.Should().NotBe(originalTab);
                WebDriverFactory.DriverContext.WindowHandles.Count.Should().Be(2);
            }
        }

        [Test]
        public void NewWindowTest()
        {
            //Arrange
            string originalWindow = WebDriverFactory.DriverContext.CurrentWindowHandle;

            //Act
            WebDriverFactory.DriverContext.OpenNewTabOrWindow(WindowType.Window);
            var windowsAfterUpdate = WebDriverFactory.DriverContext.WindowHandles;
            WebDriverFactory.DriverContext.SwitchTo().Window(originalWindow);

            //Assert
            using (new AssertionScope())
            {
                windowsAfterUpdate.Count.Should().Be(2);
                WebDriverFactory.DriverContext.CurrentWindowHandle.Should().Be(originalWindow);
            }
        }
    }
}
