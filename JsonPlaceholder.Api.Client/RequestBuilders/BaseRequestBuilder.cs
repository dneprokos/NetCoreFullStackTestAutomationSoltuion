using RestApiBase.Client.RestBase;

namespace JsonPlaceholder.Api.Client.RequestBuilders
{
    public abstract class BaseRequestBuilder
    {
        protected readonly RestClient RestClient;
        protected string SearchUrl;

        protected BaseRequestBuilder()
        {
            RestClient = new RestClient();
        }
    }
}
