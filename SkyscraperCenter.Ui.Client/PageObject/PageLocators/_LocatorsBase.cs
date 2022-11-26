using TestsBase.Client.Managers;

namespace SkyscraperCenter.Ui.Client.PageObject.PageLocators
{
    public abstract class _LocatorsBase
    {
        protected int TimeOutSeconds => TestSettingsManager.DefaultTimeOutSeconds;
    }
}
