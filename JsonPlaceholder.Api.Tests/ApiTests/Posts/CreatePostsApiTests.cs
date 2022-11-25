using FluentAssertions;
using FluentAssertions.Execution;
using JsonPlaceholder.Api.Client.ApiModels;
using JsonPlaceholder.Api.Client.Constants;
using JsonPlaceholder.Api.Client.Facade;
using JsonPlaceholder.Api.Tests.ApiTests._BaseClasses;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using RestApiBase.Client.Extensions;
using RestApiBase.Client.RestBase;
using TestsBase.Client.TestDataGenerators;

namespace JsonPlaceholder.Api.Tests.ApiTests.Posts
{

    [TestFixture, Parallelizable(ParallelScope.None)]
    [AllureSuite("Posts")]
    [AllureSubSuite("POST /posts")]
    public class CreatePostsApiTests : RestApiTestBase
    {
        [Test]
        [Parallelizable]
        public void CreatePost_WithAllProperties_ShouldBeCreated()
        {
            //Arrange
            var requestBase = JsonPlaceholderRestRequests
                .Posts()
                .WithBodyId(int.MaxValue)
                .WithBodyProperty(StringGenerator.GenerateRandomString(100))
                .WithTitle(FakeDataGenerator.Instance.LoremWord);

            //Act
            RestResponse<PostApiModelV1> postResponse = requestBase.SendPostRequest();

            //Assert
            postResponse.ShouldHaveCreatedStatusCode();
            var body = postResponse.AssertBodyIsNotNullAndThenReturn();
            using (new AssertionScope())
            {
                body.Should().BeEquivalentTo(requestBase.Model, c 
                    => c.Excluding(d => d.Id));
                body.Id.Should().NotBeNull().And.BeGreaterOrEqualTo(101); //Status is hardcoded.
            }
        }

        [Test]
        [Parallelizable]
        public void CreatePost_WithoutProperties_ShouldBeCreatedWithDefaultValues()
        {
            //Arrange
            var baseRequest = JsonPlaceholderRestRequests.Posts();

            //Act
            RestResponse<PostApiModelV1> postResponse = baseRequest.SendPostRequest();

            //Assert
            postResponse.ShouldHaveCreatedStatusCode();
            var body = postResponse.AssertBodyIsNotNullAndThenReturn();
            using (new AssertionScope())
            {
                body.Should().BeEquivalentTo(baseRequest.Model, c
                    => c.Excluding(d => d.Id));
                body.Id.Should().NotBeNull();
            }
        }

        [Test]
        [Parallelizable]
        [Description("This is fake case. It was created only for example. It will always fail")]
        public void CreatePost_WithTitleMoreThanMaxChars_ShouldBeBadRequest()
        {
            //Act
            RestResponse<PostApiModelV1> postResponse = JsonPlaceholderRestRequests
                .Posts()
                .WithTitle(StringGenerator.GenerateRandomString(PostConstants.TitleMaxChars))
                .SendPostRequest();

            //Assert
            postResponse.ShouldHaveBadRequestStatusCodeWithExpectedMessage(PostConstants.TitleMoreThanMaxValidationMessage);
        }
    }
}
