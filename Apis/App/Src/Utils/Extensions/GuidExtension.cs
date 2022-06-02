using System;

namespace InteliSystem.Utils.Extensions
{
    public static class GuidExtension
    {
        public static string ToStringKey(this Guid value)
        {
            return value.ToString();
        }

        public static string ToOnlyKey(this Guid value)
        {
            return value.ObjectToString().Replace("-", "");
        }
    }
}