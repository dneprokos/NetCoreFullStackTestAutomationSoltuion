using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Test()
        {
            int y = 0;
            int k;

            for (k = 5; k>=10; k--)
            {
                y += k;
            }

            Console.WriteLine(y);
            Console.WriteLine(k);
        }

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

        [Test]
        [Parallelizable]
        public void DelefateAndEventTestExample()
        {
            //Delegate - Contract / Agreement between publisher and subscriber
            //Determines signature of the event Handler method in Subscriber

        }

        [Test]
        [Parallelizable]
        public void Temp()
        {

            var movies = new List<Movie>()
            {
                new Movie { Name = "Avatar 2", ReleaseYear = 2023 },
                new Movie { Name = "Terminator", ReleaseYear = 1984 },
                new Movie { Name = "Terminator 2", ReleaseYear = 1991 },
                new Movie { Name = "Ant-Man and the Wasp: Quantumania", ReleaseYear = 2023 },
            };


            var skippedMovies = movies
                .GroupBy(item => item.ReleaseYear)
                .ToDictionary(grouping => grouping.Key);



        }
    }

    class Movie
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
    }

    class CustomMovie
    {
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public bool isOld { get; set; }
    }
}
