using System;
using NUnit.Framework;

namespace TestsBase.Client.Managers
{
    public class TestSettingsManager
    {
        //<-------------------Common configs------------------------------------>

        public static bool IsDebug
        {
            get
            {
                var value = TestContext.Parameters["isDebug"];
                if (value == null) return false;
                var isBoolean = bool.TryParse(value, out var isDebug);
                return isBoolean && isDebug;
            }
        }

        //<-------------------Rest API configs------------------------------------>
        public static string RestApiUrl => TestContext.Parameters["restApiBaseUrl"];
        public static string RestApiVersion => TestContext.Parameters["restApiVersion"];

        public static int DefaultApiTimeOut
        {
            get
            {
                const int defaultValue = 150;
                var defaultApiTimeOut = TestContext.Parameters["defaultApiTimeOut"];

                if (defaultApiTimeOut == null) return defaultValue;
                var isParsed = int.TryParse(defaultApiTimeOut, out int result);
                return isParsed ? result : defaultValue;
            }
        }

        //<-------------------WebDriver configs------------------------------------>
        public static string Browser => TestContext.Parameters["browser"];
        public static bool IsHeadlessMode
        {
            get
            {
                var isParsed = bool.TryParse(TestContext.Parameters["isHeadless"]!, out bool result);
                return isParsed && result;
            }
        }

        public static bool IsRemoteDriver => bool.Parse(TestContext.Parameters["isRemote"]!);
        public static Uri SeleniumHubUri => new(TestContext.Parameters["seleniumHubUrl"]!);
        public static int DefaultTimeOutSeconds
        {
            get
            {
                bool isParsed = int.TryParse(TestContext.Parameters["waitTimeOut"]!, out int result);
                return isParsed ? result : 30;
            }
        }

        //<-------------------Web Application configs------------------------------------>
        public static string BaseUrl => TestContext.Parameters["baseUrl"];
    }
}
