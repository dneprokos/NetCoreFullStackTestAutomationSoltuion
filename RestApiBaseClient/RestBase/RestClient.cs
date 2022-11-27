using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestsBase.Client.Managers;

namespace RestApiBase.Client.RestBase
{
    public class RestClient
    {
        private readonly HttpClient _client;
        private readonly bool _isDebug;
        private Dictionary<string, string> _headers;

        public RestClient()
        {
            _client = new HttpClient { Timeout = TimeSpan.FromSeconds(TestSettingsManager.DefaultApiTimeOut) };
            AddAcceptHeader("application/json");
            _isDebug = TestSettingsManager.IsDebug;
        }

        public RestClient(string authKeyName, string authKeyValue)
        {
            _client = new HttpClient { Timeout = TimeSpan.FromSeconds(TestSettingsManager.DefaultApiTimeOut) };
            AddAcceptHeader("application/json");
            AddHeader(authKeyName, authKeyValue);
            _isDebug = TestSettingsManager.IsDebug;
        }

        #region Header methods

        /// <summary>
        /// Adds default request header to request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public RestClient AddHeader(string name, string value)
        {
            _headers ??= new Dictionary<string, string>();
            _headers.Add(name, value);

            HttpRequestHeaders headers = _client.DefaultRequestHeaders;
            headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds Accept header to default headers in request
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public RestClient AddAcceptHeader(string value)
        {
            var contentType = new MediaTypeWithQualityHeaderValue(value);
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            return this;
        }

        /// <summary>
        /// Adds Bearer authorization token to request
        /// </summary>
        /// <param name="value">Authorization token you want to provide in request. Bearer will be added before this value</param>
        /// <returns></returns>
        public RestClient AddBearerToken(string value)
        {
            AddHeader("Authorization", $"Bearer {value}");
            return this;
        }

        /// <summary>
        /// Removes header by it's name
        /// </summary>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public RestClient RemoveHeader(string headerName)
        {
            _client.DefaultRequestHeaders.Remove(headerName);
            return this;
        }

        /// <summary>
        /// Removes all headers
        /// </summary>
        /// <returns></returns>
        public RestClient RemoveAllHeaders()
        {
            _client.DefaultRequestHeaders.Clear();
            return this;
        }

        #endregion

        #region Base HTTP request methods

        /// <summary>
        /// Sends GET REST Api request and return status code with expected object type
        /// </summary>
        /// <typeparam name="T">Object template you want to receive</typeparam>
        /// <param name="requestUri"></param>
        /// <param name="ignoreBodyResponse">Ignores body response when it's true</param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendGetRequest<T>(string requestUri, bool ignoreBodyResponse = false)
        {
            PrintRequestIfIsDebugMode("GET", requestUri);
            HttpResponseMessage responseMessage = await _client.GetAsync(requestUri);
            return ConvertHttpResponseToRestResponseOfT<T>(responseMessage, ignoreBodyResponse).Result;
        }

        /// <summary>
        /// Sends DELETE REST Api request and return status code with expected object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="ignoreBodyResponse"></param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendDeleteRequest<T>(string requestUri, bool ignoreBodyResponse = false)
        {
            PrintRequestIfIsDebugMode("DELETE", requestUri);
            HttpResponseMessage responseMessage = await _client.DeleteAsync(requestUri);
            return ConvertHttpResponseToRestResponseOfT<T>(responseMessage, ignoreBodyResponse).Result;
        }

        /// <summary>
        /// Sends POST REST Api request and return status code with expected object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <param name="ignoreBodyResponse"></param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendPostRequest<T>(string requestUrl, T requestBody, bool ignoreBodyResponse = false)
        {
            var (content, json) = ConvertBodyToStringContent(requestBody);
            PrintRequestIfIsDebugMode("POST", requestUrl, json);
            HttpResponseMessage responseMessage = await _client.PostAsync(requestUrl, content);
            return ConvertHttpResponseToRestResponseOfT(responseMessage, requestBody, ignoreBodyResponse).Result;
        }

        /// <summary>
        /// Sends POST REST Api request with body of type T and return status code with expected object type TM
        /// </summary>
        /// <typeparam name="T">Request body of some type</typeparam>
        /// <typeparam name="TM"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <param name="ignoreBodyResponse"></param>
        /// <returns></returns>
        public async Task<RestResponse<TM>> SendPostRequest<T, TM>(string requestUrl, T requestBody, bool ignoreBodyResponse = false)
        {
            var (content, json) = ConvertBodyToStringContent(requestBody);
            PrintRequestIfIsDebugMode("POST", requestUrl, json);
            HttpResponseMessage responseMessage = await _client.PostAsync(requestUrl, content);
            return ConvertHttpResponseToRestResponseOfT<TM>(responseMessage, ignoreBodyResponse).Result;
        }

        /// <summary>
        /// Sends PUT REST Api request and return status code with expected object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <param name="ignoreBodyResponse"></param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendPutRequest<T>(string requestUrl, T requestBody, bool ignoreBodyResponse = false)
        {
            var (content, json) = ConvertBodyToStringContent(requestBody);
            PrintRequestIfIsDebugMode("PUT", requestUrl, json);
            HttpResponseMessage responseMessage = await _client.PutAsync(requestUrl, content);

            return ConvertHttpResponseToRestResponseOfT(responseMessage, requestBody, ignoreBodyResponse).Result;
        }

        /// <summary>
        /// Sends PATCH REST Api request and return status code with expected object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="requestBody"></param>
        /// <param name="ignoreBodyResponse"></param>
        /// <returns></returns>
        public async Task<RestResponse<T>> SendPatchRequest<T>(string requestUrl, T requestBody, bool ignoreBodyResponse = false)
        {
            var (content, json) = ConvertBodyToStringContent(requestBody);
            PrintRequestIfIsDebugMode("PATCH", requestUrl, json);
            HttpResponseMessage responseMessage = await _client.PatchAsync(requestUrl, content);

            return ConvertHttpResponseToRestResponseOfT(responseMessage, requestBody, ignoreBodyResponse).Result;
        }

        public async Task<RestResponse<TM>> SendPutRequest<T, TM>(string requestUrl, T requestBody, bool ignoreBodyResponse = false)
        {
            var (content, json) = ConvertBodyToStringContent(requestBody);
            PrintRequestIfIsDebugMode("PUT", requestUrl, json);
            HttpResponseMessage responseMessage = await _client.PutAsync(requestUrl, content);
            return ConvertHttpResponseToRestResponseOfT<TM>(responseMessage, ignoreBodyResponse).Result;
        }

        public static string ConvertTypeToJson<T>(T requestBody)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.None,
            };
            return JsonConvert.SerializeObject(requestBody, settings);
        }

        #endregion

        #region Private helpers

        private static Tuple<StringContent, string> ConvertBodyToStringContent<T>(T requestBody)
        {
            var jsonObject = ConvertTypeToJson(requestBody);
            var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            return new Tuple<StringContent, string>(stringContent, jsonObject);
        }

        private async Task<RestResponse<T>> ConvertHttpResponseToRestResponseOfT<T>(HttpResponseMessage httpResponse, T body, bool ignoreBodyResponse)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            var responseBodyAsString = await httpResponse.Content.ReadAsStringAsync();
            var endpoint = httpResponse.RequestMessage.RequestUri.ToString();
            var methodName = httpResponse.RequestMessage.Method.Method;

            if (TestSettingsManager.IsDebug)
            {
                Console.WriteLine($"\tResponse Status code: {(int)statusCode}");
                if (!string.IsNullOrWhiteSpace(responseBodyAsString))
                    Console.WriteLine($"\tResponse Body: {responseBodyAsString}");
            }

            if (ignoreBodyResponse)
                return new RestResponse<T>(statusCode, endpoint, methodName, body, responseBodyAsString);

            if (!httpResponse.IsSuccessStatusCode)
                return new RestResponse<T>(statusCode, endpoint, methodName, body, responseBodyAsString);

            var jsonSerializerSettings = new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc };
            var deserializedObject = JsonConvert.DeserializeObject<T>(responseBodyAsString ?? string.Empty, jsonSerializerSettings);

            return new RestResponse<T>(statusCode, deserializedObject, endpoint, methodName, _headers);
        }

        private async Task<RestResponse<T>> ConvertHttpResponseToRestResponseOfT<T>(HttpResponseMessage httpResponse, bool ignoreBodyResponse)
        {
            HttpStatusCode statusCode = httpResponse.StatusCode;
            var responseBodyAsString = await httpResponse.Content.ReadAsStringAsync();
            var endpoint = httpResponse.RequestMessage.RequestUri.ToString();
            var methodName = httpResponse.RequestMessage.Method.Method;

            if (TestSettingsManager.IsDebug)
            {
                Console.WriteLine($"\tResponse Status code: {(int)statusCode}");
                if (!string.IsNullOrWhiteSpace(responseBodyAsString))
                    Console.WriteLine($"\tResponse Body: {responseBodyAsString}");
            }

            if (ignoreBodyResponse)
                return new RestResponse<T>(statusCode, endpoint, methodName, responseBodyAsString);

            if (!httpResponse.IsSuccessStatusCode)
                return new RestResponse<T>(statusCode, endpoint, methodName, responseBodyAsString);

            var jsonSerializerSettings = new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Utc };
            var deserializedObject = JsonConvert.DeserializeObject<T>(responseBodyAsString ?? string.Empty, jsonSerializerSettings);

            return new RestResponse<T>(statusCode, deserializedObject, endpoint, methodName, _headers);
        }

        private void PrintRequestIfIsDebugMode(string methodType, string requestUrl)
        {
            if (!_isDebug) return;

            Console.WriteLine("\n###Running API request###");
            if (_headers != null)
            {
                foreach (var (key, value) in _headers)
                {
                    Console.WriteLine($"\t{key}: {value}");
                }
            }

            Console.WriteLine($"\t{methodType} /{requestUrl} \n");
        }

        private void PrintRequestIfIsDebugMode(string methodType, string requestUrl, string body)
        {
            if (!_isDebug) return;
            Console.WriteLine("\n###Running API request###");

            if (_headers != null)
            {
                foreach (var (key, value) in _headers)
                {
                    Console.WriteLine($"\t{key}: {value}");
                }
            }

            Console.WriteLine($"\t{methodType} /{requestUrl} \n\tBody: {body}\n");
        }

        #endregion
    }
}
