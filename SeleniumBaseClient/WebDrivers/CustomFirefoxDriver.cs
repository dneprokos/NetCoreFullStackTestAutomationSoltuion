using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SeleniumBase.Client.WebDriverBase;
using System;
using TestsBase.Client.Managers;

namespace SeleniumBase.Client.WebDrivers
{
    public class CustomFirefoxDriver
    {
        public IWebDriver WebDriver { get; private set; }
        
        public CustomFirefoxDriver(WebDriverCapabilities driverOptions)
        {
            FirefoxDriverService driverService = FirefoxDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            var options = new FirefoxOptions();

            //Is Headless mode
            if (driverOptions.IsHeadless)
                options.AddArgument("headless");

            //Set window size
            string windowsSize = TestSettingsManager.WindowSize;
            string windowsSizeValue = windowsSize.ToLowerInvariant() == "full"
                ? "--start-maximized"
                : driverOptions.WindowSize;
            options.AddArgument(windowsSizeValue);

            //Is Remote or local WebDriver
            WebDriver = driverOptions.IsRemote ?
            (IWebDriver)new RemoteWebDriver(driverOptions.SeleniumHubUri, options)
            : new FirefoxDriver(driverService, options);
        }
    }
}
