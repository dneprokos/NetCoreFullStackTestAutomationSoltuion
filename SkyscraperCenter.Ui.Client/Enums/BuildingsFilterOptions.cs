using System.ComponentModel;

namespace SkyscraperCenter.Ui.Client.Enums
{
    public enum BuildingsFilterOptions
    {
        [Description("100 Tallest Completed Buildings in the World")]
        HundredTallestCompletedBuildingsInTheWorld,

        [Description("100 Tallest Under Construction Buildings in the World")]
        HundredTallestUnderConstructionBuildingsInTheWorld

        //NOTE: Can be updated with more options. This one is just for example
    }
}
