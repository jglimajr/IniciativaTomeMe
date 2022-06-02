using System.Globalization;
using System;

namespace InteliSystem.Utils.Extensions
{
    public static class NumericExtensions
    {
        public static short Sum(params short[] values)
        {
            short retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static int Sum(params int[] values)
        {
            int retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static long Sum(params long[] values)
        {
            long retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static double Sum(params double[] values)
        {
            double retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static decimal Sum(params decimal[] values)
        {
            decimal retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static double Trunc(this double value, int decimalplaces)
        {
            var valueAux = value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var parts = valueAux.Split(",");
            if (parts.Length == 1)
                return parts[0].ToDouble();
            var decimals = "";
            var virgula = "";
            var aux = parts[1];
            for (int i = 0; i < aux.Length; i++)
            {
                if (i == decimalplaces)
                    break;
                decimals += aux.Substring(i, 1);
                virgula = ",";
            }
            valueAux = string.Concat(parts[0], virgula, decimals);
            return valueAux.ToDouble();
        }
        public static double Round(this double value, int decimalplaces)
        {
            value = Math.Round(value, decimalplaces);
            return value;
        }
        public static decimal Trunc(this decimal value, int decimalplaces)
        {

            var valueAux = value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var parts = valueAux.Split(",");
            if (parts.Length == 1)
                return parts[0].ToDecimal();
            var decimals = "";
            var virgula = "";
            var aux = parts[1];
            for (int i = 0; i < aux.Length; i++)
            {
                if (i == decimalplaces)
                    break;
                decimals += aux.Substring(i, 1);
                virgula = ",";
            }
            valueAux = string.Concat(parts[0], virgula, decimals);
            return valueAux.ToDecimal();

        }
        public static decimal Round(this decimal value, int casasdecimais)
        {
            value = Math.Round(value, casasdecimais);
            return value;
        }

        public static int Round(this float value, int decimalplaces = 0)
        {
            int ret = (int)Math.Round(value, decimalplaces);
            return ret;
        }

        public static string FormatDecimal(this decimal value, int decimalplaces)
        {
            var retorno = string.Format("{0:0." + new string('0', decimalplaces) + "}", value);
            return retorno;
        }

        public static string ZeroToLeft(this object value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static string ZeroToLeft(this int value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }
        public static string ZeroToLeft(this double value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static string ZeroLeft(this decimal value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }
        public static bool IsNumeric(object value)
        {
            return double.TryParse(value.ObjectToString(), out double d);
        }

        public static float Division(this long value, params float[] values)
        {
            float ret = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (ret == 0)
                    ret = ((float)value / values[i]);
                else
                    ret = ((float)ret / value);
            }
            return ret;
        }

        public static float Division(this int value, params int[] values)
        {
            float ret = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (ret == 0)
                    ret = ((float)value / (float)values[i]);
                else
                    ret = ((float)ret / (float)value);
            }
            return ret;
        }
    }
}