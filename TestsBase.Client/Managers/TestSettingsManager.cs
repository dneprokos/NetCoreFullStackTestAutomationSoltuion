using NUnit.Framework;

namespace TestsBase.Client.Managers
{
    public class TestSettingsManager
    {
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

        public static string RestApiUrl => TestContext.Parameters["restApiBaseUrl"];
        public static string RestApiVersion => TestContext.Parameters["restApiVersion"];
    }
}
