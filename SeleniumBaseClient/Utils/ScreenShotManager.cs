using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumBase.Client.WebDriverBase;

namespace SeleniumBase.Client.Utils
{
    public class ScreenShotManager
    {
        public static string MakeScreenShot(string fileBaseName)
        {
            Screenshot screenshots = ((ITakesScreenshot)WebDriverFactory.DriverContext).GetScreenshot();
            string screenshotsPath = CreateScreenShotDirectory(fileBaseName);
            string fullName = $"{fileBaseName}-{DateTime.Now:ddMMHm}.png"; 

            string fullFilePath = Path.Combine(screenshotsPath, fullName);
            screenshots.SaveAsFile(fullFilePath, ScreenshotImageFormat.Png);

            return fullFilePath;
        }

        public static void MakeScreenOnTestFail()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                string fileName = TestContext.CurrentContext.Test.Name;
                string screenshotsPath = MakeScreenShot(fileName);
                TestContext.AddTestAttachment(screenshotsPath);
            }
        }

        #region Private helpers

        private static string CreateScreenShotDirectory(string folderName)
        {
            string directory = Path.GetDirectoryName(typeof(Screenshot).Assembly.Location);
            string path = Path.Combine(directory!, "Artifacts/Screenshots", folderName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        #endregion
    }
}
