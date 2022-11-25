using System.Collections.Generic;
using System.Net;

namespace RestApiBase.Client.RestBase
{
    public class RestResponse<T>
    {
        public HttpStatusCode StatusCode { get; }
        public T Body { get; }
        public string FullException { get; set; }
        public string RequestEndpoint { get; set; }
        public string HttpMethod { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public RestResponse(HttpStatusCode statusCode, T body, string requestEndpoint, string httpMethod, string fullException = null)
        {
            RequestEndpoint = requestEndpoint;
            HttpMethod = httpMethod;
            StatusCode = statusCode;
            Body = body;
            if (fullException != null) FullException = fullException;
        }

        public RestResponse(HttpStatusCode statusCode, T body, string requestEndpoint, string httpMethod, Dictionary<string, string> headers, string fullException = null)
        {
            RequestEndpoint = requestEndpoint;
            StatusCode = statusCode;
            Body = body;
            Headers = headers;
            HttpMethod = httpMethod;
            if (fullException != null) FullException = fullException;
        }

        public RestResponse(HttpStatusCode statusCode, string requestEndpoint, string httpMethod, string fullException = null)
        {
            RequestEndpoint = requestEndpoint;
            StatusCode = statusCode;
            HttpMethod = httpMethod;
            if (fullException != null) FullException = fullException;
        }

        public RestResponse(HttpStatusCode statusCode, string requestEndpoint, string httpMethod, T body, string fullException = null)
        {
            RequestEndpoint = requestEndpoint;
            StatusCode = statusCode;
            HttpMethod = httpMethod;
            Body = body;
            if (fullException != null) FullException = fullException;
        }
    }
}
