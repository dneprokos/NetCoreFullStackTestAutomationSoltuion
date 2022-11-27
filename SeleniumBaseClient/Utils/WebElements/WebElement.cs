using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumBase.Client.Facades;
using SeleniumBase.Client.WebDriverBase;

namespace SeleniumBase.Client.Utils.WebElements
{
    public class WebElement
    {
        #region Properties

        protected IWebElement PageElement;
        private int? _timeout;

        public string Id { get; set; }
        public string XPath { get; set; }
        public string Css { get; set; }

        public IWebElement Element => PageElement ?? FindElement();
        public IList<IWebElement> Elements => FindElements();

        #endregion

        #region Constructors

        public WebElement()
        {

        }

        public WebElement(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        public WebElement(IWebElement webElement)
        {
            this.PageElement = webElement;
        }

        #endregion

        #region Find Elements

        internal IWebElement FindElement()
        {
            return FindElement(WebDriverFactory.DriverContext.WaitForElement);
        }

        internal List<IWebElement> FindElements()
        {
            return FindElement
                (WebDriverFactory.DriverContext.WaitForElements).ToList();
        }

        public T FindElement<T>(Func<By, int, T> func, int timeOut = 30)
        {
            By findBy;

            if (!string.IsNullOrWhiteSpace(this.Id))
            {
                findBy = By.Id(this.Id);
            }
            else if (!string.IsNullOrWhiteSpace(this.XPath))
            {
                findBy = By.XPath(this.XPath);
            }
            else if (!string.IsNullOrWhiteSpace(this.Css))
            {
                findBy = By.CssSelector(this.Css);
            }
            else
            {
                throw new NotFoundException("Locator was not found. Please make sure you've passed it");
            }

            try
            {
                return func.Invoke(findBy, this._timeout ?? timeOut);
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"{this} element was not found");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"{this} element was not found during {timeOut} seconds");
            }
        }

        #endregion

        #region Find Child elements

        public T FindChildElement<T>(WebElement wenElement, int timeOut = 30)
        {
            By findBy;

            if (!string.IsNullOrWhiteSpace(wenElement.Id))
            {
                findBy = By.Id(wenElement.Id);
            }
            else if (!string.IsNullOrWhiteSpace(wenElement.XPath))
            {
                findBy = By.XPath(wenElement.XPath);
            }
            else if (!string.IsNullOrWhiteSpace(wenElement.Css))
            {
                findBy = By.CssSelector(wenElement.Css);
            }
            else
            {
                throw new NotFoundException("Locator was not found. Please make sure you've passed it");
            }

            try
            {
                var childElement = this.Element.WaitForElement(findBy, timeOut);
                T childElementOfTypeT =
                    (T)Convert.ChangeType(
                        (T)Activator.CreateInstance(typeof(T), childElement),
                        typeof(T));
                return childElementOfTypeT;
            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"{this} element was not found");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"{this} element was not found during {timeOut} seconds");
            }
        }

        public T FindChildElement<T>(By findBy, int timeOut = 30)
        {
            try
            {
                IWebElement childElement = this.Element.WaitForElement(findBy, timeOut);
                T childElementOfT = (T)Convert.ChangeType(
                        (T)Activator.CreateInstance(typeof(T), childElement),
                        typeof(T));
                return childElementOfT;

            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"{this} element was not found");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"{this} element was not found during {timeOut} seconds");
            }
        }

        public IList<T> FindChildElements<T>(WebElement webElement, int timeOut = 30) where T : WebElement
        {
            By findBy;

            if (!string.IsNullOrWhiteSpace(webElement.Id))
            {
                findBy = By.Id(webElement.Id);
            }
            else if (!string.IsNullOrWhiteSpace(webElement.XPath))
            {
                findBy = By.XPath(webElement.XPath);
            }
            else if (!string.IsNullOrWhiteSpace(webElement.Css))
            {
                findBy = By.CssSelector(webElement.Css);
            }
            else
            {
                throw new NotFoundException("Locator was not found. Please make sure you've passed it");
            }

            var elementsListOfT = new List<WebElement>();

            try
            {
                IReadOnlyCollection<IWebElement> childElements
                    = Element.WaitForElements(findBy, timeOut);

                foreach (var childElement in childElements)
                {
                    elementsListOfT.Add((T)Convert.ChangeType(
                        (T)Activator.CreateInstance(typeof(T), childElement),
                        typeof(T)));
                }

                return elementsListOfT.OfType<T>().ToList();

            }
            catch (NoSuchElementException)
            {
                throw new NoSuchElementException($"Child webelement {webElement} was not found in parent {this}");
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"{this} element was not found during {timeOut} seconds");
            }
        }

        #endregion

        #region WebElement helpers

        public bool IsExist()
        {
            try
            {
                _timeout = 0;
                return Element != null;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _timeout = null;
            }

        }

        public string GetInnerText()
        {
            return SeleniumFramework
                .JavaScriptHelper.RunJavaScript("return arguments[0].innerText; ", this.PageElement) as string;
        }

        #endregion


    }
}
