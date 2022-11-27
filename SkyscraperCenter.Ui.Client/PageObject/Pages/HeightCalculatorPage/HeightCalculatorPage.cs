using SkyscraperCenter.Ui.Client.PageObject.PageLocators.HeightCalculatorPage;

namespace SkyscraperCenter.Ui.Client.PageObject.Pages.HeightCalculatorPage
{
    public class HeightCalculatorPage
    {
        private readonly HeightCalculatorPageLocators _locators;

        public HeightCalculatorPage()
        {
            _locators = new HeightCalculatorPageLocators();
        }

        public HeightCalculatorPage EnterTextToFloorsAboveGroundInputField(string text)
        {
            _locators.FloorsAboveGroundInputField.SendKeys(text);
            return this;
        }

        public HeightCalculatorPage EnterTextToHeightInputField(string text)
        {
            _locators.HeightInputFieldRelativePath.SendKeys(text);
            return this;
        }

        public string ReadHeightInputTextValue()
        {
            return _locators.HeightInputFieldRelativePath.GetAttribute("value");
        }
    }
}
