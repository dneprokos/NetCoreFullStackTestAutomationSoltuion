using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumBase.Client.WebDriverBase;
using System;
using TestsBase.Client.Managers;

namespace SeleniumBase.Client.WebDrivers
{
    public class CustomChromeDriver
    {
        public IWebDriver WebDriver { get; private set; }

        public CustomChromeDriver(WebDriverCapabilities driverOptions)
        {
            ChromeDriverService driverService
                        = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");

            //Is Headless mode
            if (driverOptions.IsHeadless)
                options.AddArgument("headless");

            //Set window size
            string windowsSize = TestSettingsManager.WindowSize;
            string windowsSizeValue = windowsSize.ToLowerInvariant() == "full"
                ? "start-maximized"
                : driverOptions.WindowSize;
            options.AddArgument(windowsSizeValue);

            //Is Remote or local WebDriver
            WebDriver = driverOptions.IsRemote ?
                (IWebDriver)new RemoteWebDriver(driverOptions.SeleniumHubUri, options)
                : new ChromeDriver(driverService, options);
        }
    }
}
