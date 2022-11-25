using JsonPlaceholder.Api.Client.RequestBuilders.PostBuilders;

namespace JsonPlaceholder.Api.Client.Facade
{
    public class JsonPlaceholderRestRequests
    {
        public static PostsRequestBuilder Posts() => new PostsRequestBuilder();
    }
}
