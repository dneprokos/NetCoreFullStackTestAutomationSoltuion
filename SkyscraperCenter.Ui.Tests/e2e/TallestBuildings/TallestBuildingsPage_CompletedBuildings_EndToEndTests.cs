using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using SkyscraperCenter.Ui.Client.Constants;
using SkyscraperCenter.Ui.Client.Facades;
using SkyscraperCenter.Ui.Client.PageObject.Pages.BuildingPage.PageComponents;
using SkyscraperCenter.Ui.Tests.e2e._TestsBase;

namespace SkyscraperCenter.Ui.Tests.e2e.TallestBuildings
{
    [Parallelizable(ParallelScope.All)]
    [AllureSuite("TallestBuildingsPage")]
    [AllureSubSuite("100 Tallest Completed Buildings in the World")]
    // ReSharper disable once InconsistentNaming
    public class TallestBuildingsPage_CompletedBuildings_EndToEndTests : UiTestsBase
    {
        [SetUp]
        public void BeforeEachTest()
        {
            //NOTE: hadcoded string was specified only for testing purposes. Page Url constant exist
            SkyscraperCenterBase.QuickNavigation.GoToPage(PageUrls.TallestBuildingsWithFilterUnderConstructionPageUrl); 
            SkyscraperCenterBase.PageObjects.BuildingsPage.SelectFilterDropDownByText(
                "100 Tallest Completed Buildings in the World", true);
        }

        [TestCase(100)]
        public void TallestBuildingsTable_TallestCompleted_ShouldContainExpectedNumberOfTheRecords(int expectedRecordsCount)
        {
            //Arrange

            //Act
            int actualRecordsCount = SkyscraperCenterBase
                .PageObjects
                .BuildingsPage
                .BuildingsTable
                .GetRecordsCount();

            //Assert
            actualRecordsCount.Should().Be(expectedRecordsCount);
        }

        [TestCase("Lotte World Tower", 123)]
        public void TallestBuildingsTable_TallestCompleted_BuildingShouldHaveExpectedNumberOfTheFloors(string buildingName, int expectedFloors)
        {
            //Arrange
            List<BuildingsTableComponentModel> table = SkyscraperCenterBase
                .PageObjects
                .BuildingsPage
                .BuildingsTable
                .GetStaticTableRecords();

            //Act
            BuildingsTableComponentModel searchBuilding = table.FirstOrDefault(r => r.NAME == buildingName);

            //Assert
            searchBuilding.Should().NotBeNull($"Building with name {buildingName} was not found in the table");
            searchBuilding!.FLOORS.Should().Be(expectedFloors);
        }

        [Test]
        public void TallestBuildingsTable_TallestCompleted_VerifyBuildingWithMaximumNumberOfFloors()
        {
            //Arrange
            const int expectedFlours = 163;
            const string expectedName = "Burj Khalifa";
            List<BuildingsTableComponentModel> table = SkyscraperCenterBase
                .PageObjects
                .BuildingsPage
                .BuildingsTable
                .GetStaticTableRecords();

            //Act
            BuildingsTableComponentModel buildingWithMaxFloors = table
                .Aggregate((max, next) 
                    => next.FLOORS > max.FLOORS ? next : max);

            //Assert
            using (new AssertionScope())
            {
                buildingWithMaxFloors.NAME.Should().Be(expectedName);
                buildingWithMaxFloors.FLOORS.Should().Be(expectedFlours);
            }
        }

    }
}
