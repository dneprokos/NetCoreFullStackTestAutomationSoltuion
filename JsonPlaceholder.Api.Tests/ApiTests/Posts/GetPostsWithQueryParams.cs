using FluentAssertions;
using FluentAssertions.Execution;
using JsonPlaceholder.Api.Client.Facade;
using JsonPlaceholder.Api.Tests.ApiTests._BaseClasses;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using RestApiBase.Client.Extensions;

namespace JsonPlaceholder.Api.Tests.ApiTests.Posts
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    [AllureSuite("Posts")]
    [AllureSubSuite("GET /posts")]
    public class GetPostsWithQueryParams : RestApiTestBase
    {
        [Test]
        public void GetPostsWithQueryParam_UserId_ShouldReturnFilteredResults()
        {
            //Arrange
            //NOTE: Let's imagine we took expected results from DB
            const int userId = 1; 
            const int expectedResultsCount = 10;

            //Act
            var getResponse = JsonPlaceholderRestRequests
                .Posts()
                .WithQueryUserId(userId)
                .SendGetRequest();

            //Assert
            getResponse.ShouldHaveOkStatusCode();
            var body = getResponse.AssertBodyIsNotNullAndThenReturn();
            using (new AssertionScope())
            {
                body.ForEach(record => record.UserId.Should().Be(userId));
                body.Should().HaveCount(expectedResultsCount);
            }

            var sql = "select top(1) from Orders";


        }
    }
}
