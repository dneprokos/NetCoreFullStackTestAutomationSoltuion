using System.Collections.Generic;
using FluentAssertions;
using Flurl;
using JsonPlaceholder.Api.Client.ApiModels;
using JsonPlaceholder.Api.Client.EndpointUrls;
using RestApiBase.Client.RestBase;

namespace JsonPlaceholder.Api.Client.RequestBuilders.PostBuilders
{
    public class PostsRequestBuilder : BaseRequestBuilder
    {
        public PostApiModelV1 Model;

        public PostsRequestBuilder()
        {
            Model = new PostApiModelV1();
            SearchUrl = Endpoints.Posts();
        }

        #region Body builder

        public PostsRequestBuilder WithBodyId(int? id)
        {
            Model.Id = id;
            return this;
        }

        public PostsRequestBuilder WithTitle(string body)
        {
            Model.Title = body;
            return this;
        }

        public PostsRequestBuilder WithBodyProperty(string body)
        {
            Model.Body = body;
            return this;
        }

        public PostsRequestBuilder WithBodyUserId(int? userId)
        {
            Model.UserId = userId;
            return this;
        }

        #endregion

        #region Query builder

        public PostsRequestBuilder WithQueryUserId(int userId)
        {
            SearchUrl = SearchUrl.SetQueryParam("userId", userId);
            return this;
        }

        #endregion

        #region Send requests

        public RestResponse<PostApiModelV1> SendPostRequest()
        {
            return RestClient.SendPostRequest(Endpoints.Posts(), Model).Result;
        }

        public RestResponse<PostApiModelV1> SendPutRequest(int? id)
        {
            id.Should().NotBeNull();
            // ReSharper disable once PossibleInvalidOperationException
            return RestClient.SendPutRequest(Endpoints.Posts(id.Value), Model).Result;
        }

        public RestResponse<PostApiModelV1> SendGetByIdRequest(int? id)
        {
            id.Should().NotBeNull();
            // ReSharper disable once PossibleInvalidOperationException
            return RestClient.SendGetRequest<PostApiModelV1>(Endpoints.Posts(id.Value)).Result;
        }

        public RestResponse<List<PostApiModelV1>> SendGetRequest()
        {
            return RestClient.SendGetRequest<List<PostApiModelV1>>(SearchUrl).Result;
        }

        public RestResponse<PostApiModelV1> SendDeleteRequest(int id)
        {
            return RestClient.SendDeleteRequest<PostApiModelV1>(Endpoints.Posts(id)).Result;
        }

        #endregion
    }
}
