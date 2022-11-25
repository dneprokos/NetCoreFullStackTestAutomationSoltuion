using System.Net;
using FluentAssertions;
using RestApiBase.Client.RestBase;

namespace RestApiBase.Client.Extensions
{
    public static class RestResponseAssertExtension
    {
        public static void ShouldHaveExpectedStatusCode<T>(this RestResponse<T> response, HttpStatusCode expectedStatusCode)
        {
            response.StatusCode.Should().Be(expectedStatusCode, BuildFailException(response));
        }

        public static void ShouldHaveCreatedStatusCode<T>(this RestResponse<T> response)
        {
            response.ShouldHaveExpectedStatusCode(HttpStatusCode.Created);
        }

        public static void ShouldHaveOkStatusCode<T>(this RestResponse<T> response)
        {
            response.ShouldHaveExpectedStatusCode(HttpStatusCode.OK);
        }

        public static void ShouldHaveBadRequestStatusCodeWithExpectedMessage<T>(this RestResponse<T> response, string expectedMessage)
        {
            response.ShouldHaveExpectedStatusCode(HttpStatusCode.BadRequest);
            response.FullException.Should().Contain(expectedMessage);
        }

        public static void ShouldHaveNotFoundStatusCodeWithExpectedMessage<T>(this RestResponse<T> response, string expectedMessage)
        {
            response.ShouldHaveExpectedStatusCode(HttpStatusCode.NotFound);
            response.FullException.Should().Contain(expectedMessage);
        }

        public static T AssertBodyIsNotNullAndThenReturn<T>(this RestResponse<T> response)
        {
            response.Body.Should().NotBeNull($"Exception: {response.FullException}\nRequestUrl: {response.HttpMethod} {response.RequestEndpoint}");
            return response.Body;
        }


        #region Private helpers

        private static string BuildFailException<T>(RestResponse<T> response)
        {
            var message = $"Exception: {response.FullException}\nRequestUrl: {response.HttpMethod} {response.RequestEndpoint}";
            var httpMethod = response.HttpMethod;

            if (httpMethod == "POST" || httpMethod == "PUT")
            {
                message += $" \nRequest Body: { RestClient.ConvertTypeToJson(response.Body) }\n";
            }

            return message;
        }

        #endregion
    }
}
