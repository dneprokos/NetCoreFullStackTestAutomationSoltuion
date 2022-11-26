using System.Threading;
using NUnit.Framework;
using SkyscraperCenter.Ui.Client.Constants;
using SkyscraperCenter.Ui.Client.Facades;
using SkyscraperCenter.Ui.Tests.e2e._TestsBase;

namespace SkyscraperCenter.Ui.Tests.e2e.TallestBuildings
{
    [Parallelizable(ParallelScope.All)]
    public class TallestBuildingsPageEndToEndTests : UiTestsBase
    {
        [SetUp]
        public void BeforeEachTest()
        {
            SkyscraperCenterBase.QuickNavigation.GoToPage(PageUrls.TallestBuildingsPageUrl);
            SkyscraperCenterBase.PageObjects.BuildingsPage.SelectFilterDropDownByText(
                "100 Tallest Completed Buildings in the World");
        }

        [TestCase(100)]
        public void TallestBuildingsTable_TallestCompleted_ShouldContainExpectedNumberOfTheRecords(int expectedRecordsCount)
        {
            //Arrange

            //Act
            Thread.Sleep(4000);

            //Assert
        }

        [TestCase("Lotte World Tower", 123)]
        public void TallestBuildingsTable_TallestCompleted_BuildingShouldHaveExpectedNumberOfTheFloors(string buildingName, int expectedFloors)
        {
            //Arrange

            //Act
            Thread.Sleep(5000);

            //Assert
        }

        [Test]
        public void TallestBuildingsTable_TallestCompleted_VerifyBuildingWithMaximumNumberOfFloors()
        {
            //Arrange

            //Act

            //Assert
            Thread.Sleep(3000);
        }

    }
}
