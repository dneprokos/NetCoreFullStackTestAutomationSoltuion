using FluentAssertions;
using JsonPlaceholder.Api.Client.ApiModels;
using JsonPlaceholder.Api.Client.Constants;
using JsonPlaceholder.Api.Client.Facade;
using JsonPlaceholder.Api.Tests.ApiTests._BaseClasses;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using RestApiBase.Client.Extensions;

namespace JsonPlaceholder.Api.Tests.ApiTests.Posts
{
    [TestFixture, Parallelizable(ParallelScope.All)]
    [AllureSuite("Posts")]
    [AllureSubSuite("GET /posts/id")]
    public class GetPostsByIdApiTests : RestApiTestBase
    {
        [Test]
        public void GetPostById_PostExists_ShouldBeReturned()
        {
            //Arrange
            var existRecord = new PostApiModelV1
            {
                Id = 1,
                Title = "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
                Body =
                    "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto",
                UserId = 1
            }; //NOTE: Let's imagine we created it or know it exists

            //Act
            var getResponse = JsonPlaceholderRestRequests
                .Posts()
                .SendGetByIdRequest(existRecord.Id);

            //Assert
            getResponse.ShouldHaveOkStatusCode();
            var body = getResponse.AssertBodyIsNotNullAndThenReturn();
            body.Should().BeEquivalentTo(existRecord);
        }

        [Test]
        [Description("Will also fail, because message was not designed in the API example")]
        public void GetPostById_PostDoesNotExist_ShouldBeNotFound()
        {
            //Arrange
            const int idDoesNotExist = int.MaxValue;

            //Act
            var getResponse = JsonPlaceholderRestRequests
                .Posts()
                .SendGetByIdRequest(idDoesNotExist);

            //Assert
            getResponse
                .ShouldHaveNotFoundStatusCodeWithExpectedMessage(
                    PostConstants.RecordWasNotFoundMessage(idDoesNotExist));
        }
    }
}
