﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using SeleniumBase.Client.Utils;
using SeleniumBase.Client.WebDriverBase;

namespace SkyscraperCenter.Ui.Client.PageObject.PageLocators.BuildingPage
{
    public class BuildingsPageLocators : _LocatorsBase
    {
        public IWebElement SelectFilterBase =>
            WebDriverFactory.DriverContext.WaitForElement(By.CssSelector("select.select-lists-pages"), TimeOutSeconds);

        public List<IWebElement> Headers =>
            WebDriverFactory.DriverContext
                .WaitForElements(By.CssSelector("table#buildingsTable > thead > tr > th >div > p"), TimeOutSeconds)
                .ToList();

        public List<IWebElement> RowElements => WebDriverFactory.DriverContext
            .WaitForElements(By.CssSelector("table#buildingsTable > tbody > tr"), TimeOutSeconds)
            .ToList();

    }
}