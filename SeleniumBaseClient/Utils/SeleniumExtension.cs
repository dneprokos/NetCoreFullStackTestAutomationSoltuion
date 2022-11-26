using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumBase.Client.WebDriverBase;
using SeleniumExtras.WaitHelpers;

namespace SeleniumBase.Client.Utils
{
    public static class SeleniumExtension
    {
        public static IWebElement WaitForElement(this IWebDriver element, By locator,
            int secondsTimeOut)
        {
            if (secondsTimeOut <= 0)
                return element.FindElement(locator);

            var wait = new WebDriverWait(
                WebDriverFactory.DriverContext,
                TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => element.FindElement(locator));
            }
            catch (WebDriverTimeoutException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        public static IReadOnlyCollection<IWebElement> WaitForElements(this IWebDriver driver, By locator,
            int secondsTimeOut)
        {
            if (secondsTimeOut <= 0)
                return driver.FindElements(locator);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsTimeOut));

            try
            {
                return wait.Until(dr => dr.FindElements(locator));
            }
            catch (WebDriverTimeoutException exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Enters into the Frame
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="frameNameOrId"></param>
        /// <param name="secondsTimeOut"></param>
        public static void SwitchToFrame(this IWebDriver webDriver, string frameNameOrId, int secondsTimeOut)
        {
            webDriver.WaitUntilFrameIsAvailable(frameNameOrId, secondsTimeOut);
        }

        /// <summary>
        /// Exists from Frame to default DOM content
        /// </summary>
        /// <param name="webDriver"></param>
        public static void SwitchToDefaultContent(this IWebDriver webDriver)
        {
            webDriver.SwitchTo().DefaultContent();
        }

        public static void HoverMouseOverElement(this IWebElement element)
        {
            var action = new Actions(WebDriverFactory.DriverContext);
            action.MoveToElement(element).Perform();
        }

        public static IAlert SwitchToAlert(this IWebDriver webDriver)
        {
            return webDriver.SwitchTo().Alert();
        }

        public static string WaitForElementNotVisible(this IWebDriver driver, By locator, int timeOutInSeconds)
        {
            if (driver == null || locator == null)
            {

                return "WebDriver or Locator is null";
            }
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                return null;
            }
            catch (TimeoutException)
            {
                return $"Element still visible after {timeOutInSeconds} seconds";
            }
        }

        public static string WaitUntilFrameIsAvailable(this IWebDriver driver, string frameId, int timeOutInSeconds)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameId));
                return null;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string WaitUntilElementIsClickable(this IWebDriver driver, By locator, int timeOutInSeconds)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    .Until(ExpectedConditions.ElementToBeClickable(locator));
                return null;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
