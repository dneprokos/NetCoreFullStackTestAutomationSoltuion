using System;
using OpenQA.Selenium;
using SeleniumBase.Client.WebDriverBase;

namespace SeleniumBase.Client.Utils
{
    public class JsHelper
    {
        /// <summary>
        /// Runs javascript in the current browser and returns result
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object RunJavaScript(string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)WebDriverFactory.DriverContext;
            try
            {
                return jsExec.ExecuteScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"JavaScript {script} execution was failed");
                return false;
            }
        }

        /// <summary>
        /// Run Async javascript in the current browser
        /// </summary>
        /// <param name="script"></param>
        /// <param name="parameters"></param>
        public void RunJavaScriptAsync(string script, params object[] parameters)
        {
            var jsExec = (IJavaScriptExecutor)WebDriverFactory.DriverContext;
            try
            {
                jsExec.ExecuteAsyncScript(script, parameters);
            }
            catch (Exception)
            {
                Console.WriteLine($"Async JavaScript {script} execution was failed");
            }
        }
    }
}
