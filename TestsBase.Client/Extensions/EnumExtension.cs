using System;
using System.ComponentModel;

namespace TestsBase.Client.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum myEnum)
        {
            var oFieldInfo = myEnum.GetType().GetField(myEnum.ToString());
            var attributes = (DescriptionAttribute[])oFieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : myEnum.ToString();
        }
    }
}
