using System;

namespace SeleniumBase.Client.WebDriverBase
{
    public class WebDriverCapabilities
    {
        public string WindowSize { get; set ; }
        public string Browser { get; set; }
        public bool IsHeadless { get; set; }
        public bool IsRemote { get; set; }
        public Uri SeleniumHubUri { get; set; }
    }
}
