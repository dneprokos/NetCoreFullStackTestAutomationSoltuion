using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestsBase.Client.Managers;

namespace SeleniumBase.Client.WebDriverBase
{
    public class WebDriverFactory
    {
        #region Public members

        public static IWebDriver DriverContext
        {
            get
            {
                return WebDriversCollection.First(collection => collection.Value == TestContext.CurrentContext.Test.ID).Key;
            }
            set => WebDriversCollection.TryAdd(value, TestContext.CurrentContext.Test.ID);
        }

        private IWebDriver InstantiateWebDriver(WebDriverCapabilities driverOptions)
        {
            switch (driverOptions.Browser)
            {
                case nameof(SupportedBrowsers.Chrome):
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
                        DriverContext = driverOptions.IsRemote ?
                            (IWebDriver)new RemoteWebDriver(driverOptions.SeleniumHubUri, options)
                            : new ChromeDriver(driverService, options);

                        break;
                    }
            }

            return DriverContext;
        }

        public void StartDriver(WebDriverCapabilities driverOptions)
        {
            if (driverOptions.Equals(null))
            {
                throw new ArgumentException(MethodBase.GetCurrentMethod()!.Name);
            }

            lock (CollectionLocker)
            {
                if (WebDriversCollection.Any(dict => dict.Value.Equals(string.Empty)))
                {
                    IWebDriver driver = WebDriversCollection.First(dict => dict.Value.Equals(string.Empty)).Key;
                    WebDriversCollection.TryUpdate(driver, TestContext.CurrentContext.Test.ID, string.Empty);

                    return;
                }
            }

            DriverContext = InstantiateWebDriver(driverOptions);
            WebDriversCollection.TryAdd(DriverContext, TestContext.CurrentContext.Test.ID);
        }

        public void StopDriver()
        {
            IWebDriver currentWebDriver = WebDriversCollection
                .First(dict => dict.Value.Equals(TestContext.CurrentContext.Test.ID))
                .Key;

            //Quits driver in case of exception
            if (TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)
                && TestContext.CurrentContext.Result.Message!.Contains("WebDriverException"))
            {
                currentWebDriver.Quit();
                WebDriversCollection.TryRemove(currentWebDriver, out _);
            }

            WebDriversCollection
                .TryUpdate(currentWebDriver, string.Empty, TestContext.CurrentContext.Test.ID);
        }

        public void QuitAndKillChromeProcesses()
        {
            var timer = new Stopwatch();
            timer.Start();

            while (WebDriversCollection.Values.Any(driver => !string.IsNullOrEmpty(driver)) && timer.Elapsed.Seconds < 60)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1)); //TODO: Need to fix in the future
            }

            WebDriversCollection.ToList().ForEach(pair => pair.Key.Quit());
            WebDriversCollection.Clear();

            List<int> chromeProcessesIds = Process.GetProcessesByName("chrome")
                .Select(process => process.Id)
                .ToList();

            List<int> processIds = chromeProcessesIds.Except(RunningChromeProcesses).ToList();

            processIds.ForEach(processId =>
            {
                try
                {
                    Process.GetProcessById(processId).Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public void KillChromeProcesses(string processName = "chromedriver", bool applyForCreatedOnly = true)
        {
            foreach (Process process in Process.GetProcessesByName(processName))
            {
                string testRunPath = string.Concat(Directory.GetCurrentDirectory(), @"\", $"{processName}.exe");
                string processPath = process.MainModule!.FileName;

                if (applyForCreatedOnly && testRunPath != processPath)
                    continue;

                try
                {
                    process.Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #endregion

        #region Private members

        private static readonly ConcurrentDictionary<IWebDriver, string> WebDriversCollection
            = new ConcurrentDictionary<IWebDriver, string>();

        private static readonly object CollectionLocker = new object();

        private static readonly List<int> RunningChromeProcesses = Process.GetProcessesByName("chrome")
            .Select(process => process.Id)
            .ToList();

        #endregion
    }
}
