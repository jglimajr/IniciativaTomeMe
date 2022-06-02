using System;

namespace InteliSystem.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool ValidPeriodOfTime(this DateTime value, TimeSpan compare, int minutes)
        {
            var minutesAux = (compare - value.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }

        public static bool ValidPeriodOfTime(this DateTime value, DateTime compare, int minutes)
        {
            if (value.AddMinutes(minutes).ToString("ddMMyyyy") != compare.AddMinutes(minutes).ToString("ddMMyyyy"))
                return false;
            var minutesAux = (compare.TimeOfDay - value.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }

        public static bool ValidPeriodOfTime(this DateTime? value, TimeSpan compare, int minutes)
        {
            var minutesAux = (compare - value?.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }

        public static bool ValidPeriodOfTime(this DateTime? value, DateTime compare, int minutes)
        {
            if (value?.AddMinutes(minutes).ToString("ddMMyyyy") != compare.AddMinutes(minutes).ToString("ddMMyyyy"))
                return false;
            var minutesAux = (compare.TimeOfDay - value?.TimeOfDay);
            var comparar = TimeSpan.FromMinutes(minutes++);
            return (minutesAux < comparar);
        }

        public static bool Between(this DateTime value, DateTime datestart, DateTime dateend)
        {
            return (value >= datestart && value <= dateend);
        }

        public static bool Between(this DateTime? value, DateTime datestat, DateTime dateend)
        {
            if (value.IsNull())
                return true;

            return (value >= datestat && value <= dateend);
        }


        public static string ToBrazilianDateTimeString(this DateTime value)
        {
            return ToDateTime(value);
        }
        public static string ToBrazilianDateString(this DateTime value)
        {
            return ToBrazilianDate(value);
        }

        public static string ToBrazilianDateString(this DateTime? value)
        {
            if (value == null)
                return "__/__/____";

            return ToBrazilianDate((DateTime)value);
        }

        private static string ToBrazilianDate(DateTime value)
        {
            var day = value.Day.ZeroToLeft(2);
            var month = value.Month.ZeroToLeft(2);
            var year = value.Year.ZeroToLeft(4);
            return $"{day}/{month}/{year}";
        }

        private static string ToDateTime(DateTime value)
        {
            var data = ToBrazilianDate(value);
            return $"{data} {value.ToString("HH:mm:ss")}";
        }
    }
}