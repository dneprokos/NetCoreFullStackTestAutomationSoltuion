using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SkyscraperCenter.Ui.Client.Constants;
using SkyscraperCenter.Ui.Client.Enums;
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
            SkyscraperCenterBase.Pages.BuildingsPage
                .SelectFilterDropDownByText(
                    BuildingsFilterOptions.HundredTallestCompletedBuildingsInTheWorld, true);
        }

        [TestCase(100)]
        public void TallestBuildingsTable_TallestCompleted_ShouldContainExpectedNumberOfTheRecords(int expectedRecordsCount)
        {
            //Arrange

            //Act
            int actualRecordsCount = SkyscraperCenterBase
                .Pages
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

            //Act
            List<BuildingsTableComponentModel> table = SkyscraperCenterBase
                .Pages
                .BuildingsPage
                .BuildingsTable
                .GetTableWithStaticColumns();

            //Assert
            BuildingsTableComponentModel searchBuilding = table.FirstOrDefault(r => r.NAME == buildingName);
            searchBuilding.Should().NotBeNull($"Building with name {buildingName} was not found in the table");
            searchBuilding!.FLOORS.Should().Be(expectedFloors);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TallestBuildingsTable_TallestCompleted_VerifyBuildingWithMaximumNumberOfFloors(int approach)
        {
            //Arrange
            const int expectedFlours = 163;
            const string expectedName = "Burj Khalifa";
            List<BuildingsTableComponentModel> table = SkyscraperCenterBase
                .Pages
                .BuildingsPage
                .BuildingsTable
                .GetTableWithStaticColumns();

            //Act
            BuildingsTableComponentModel buildingWithMaxFloors = null;
            //NOTE: This one was only created for only to show a LINQ examples. Please use only one approach in the real world!
            switch (approach)
            {
                case 1:
                    buildingWithMaxFloors = table
                        .Aggregate((max, next)
                            => next.FLOORS > max.FLOORS ? next : max);
                    break;
                case 2:
                    buildingWithMaxFloors = table.OrderByDescending(data => data.FLOORS).First();
                    break;
                case 3:
                    // ReSharper disable once PossibleInvalidOperationException
                    int maxValue = (int)table.Max(data => data.FLOORS);
                    buildingWithMaxFloors = table.First(data => data.FLOORS == maxValue);
                    break;
                default:
                    Assert.Fail("No such approach available. Please make sure you have specified correct approach parameter");
                    break;
            }

            //Assert
            using (new AssertionScope())
            {
                buildingWithMaxFloors!.NAME.Should().Be(expectedName);
                buildingWithMaxFloors.FLOORS.Should().Be(expectedFlours);
            }
        }
    }
}



