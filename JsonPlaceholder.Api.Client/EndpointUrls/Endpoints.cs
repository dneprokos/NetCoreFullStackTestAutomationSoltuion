using System;
using TestsBase.Client.Managers;

namespace JsonPlaceholder.Api.Client.EndpointUrls
{
    public class Endpoints
    {
        private static readonly string BaseUrl = TestSettingsManager.RestApiUrl == null ?
            throw new Exception("restApiBaseUrl cannot be null. Please make sure it was specified in .runsettings file") :
            TestSettingsManager.RestApiUrl;

        #region Posts

        public static string Posts(int id)
            => $"{BaseUrl}/posts/{id}";

        public static string Posts()
            => $"{BaseUrl}/posts";

        #endregion
    }
}
