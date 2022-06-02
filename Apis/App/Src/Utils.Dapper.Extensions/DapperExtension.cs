using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using InteliSystem.Utils.Dapper.Extensions.Attributes;
using Dapper;
using System.Threading;

namespace InteliSystem.Utils.Dapper.Extensions
{
    public static class DapperExtension
    {
        public static Task<IEnumerable<T>> GetAllAsync<T>(this IDbConnection conn, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var scolumns = "";
            var comma = "";
            // var Id = "";
            // var swhere = "";
            var columns = GetPropertyNames(typeof(T));

            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);

                if (!colname.ToUpper().Equals("Notifications".ToUpper()))
                {
                    if (!colname.ToUpper().Equals("Valid".ToUpper()))
                    {
                        if (!colname.ToUpper().Equals("Invalid".ToUpper()))
                        {
                            // if (IsPropertyKey(column))
                            //     Id = " [Id]";

                            scolumns += $"{comma}[{colname}]";
                            comma = ", ";
                            // Id = "";
                        }
                    }
                }

            });

            var sSql = $"Select {scolumns} From {GetTableName(typeof(T))}";

            return conn.QueryAsync<T>(sSql);
        }

        public static Task<T> GetAsync<T>(this IDbConnection conn, T tobjct, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var scolumns = "";
            var comma = "";
            var swhere = "";
            var Id = "";
            var columns = GetPropertyNames(typeof(T));
            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                if (!colname.ToUpper().Equals("Notifications".ToUpper()))
                {
                    if (!colname.ToUpper().Equals("Valid".ToUpper()))
                    {
                        if (!colname.ToUpper().Equals("Invalid".ToUpper()))
                        {


                            if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                            {
                                // swhere = $"{colname} = @Id";
                                Id = " [Id]";
                                swhere = $"{colname} = @{colname}";
                            }
                            scolumns += $"{comma}[{colname}]{Id}";
                            comma = ", ";
                            Id = "";
                        }
                    }
                }

            });

            var sSql = $"Select {scolumns} From {GetTableName(typeof(T))} Where {swhere}";

            return conn.QueryFirstOrDefaultAsync<T>(sSql, tobjct);
        }
        public static Task<int> InsertAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var scolumns = "";
            var svalues = "";
            var comma = "";
            var columns = GetPropertyNames(typeof(T));
            columns.Where(IsInsertable).ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);

                if (!colname.ToUpper().Equals("Notifications".ToUpper()))
                {
                    if (!colname.ToUpper().Equals("Valid".ToUpper()))
                    {
                        if (!colname.ToUpper().Equals("Invalid".ToUpper()))
                        {
                            scolumns += $"{comma}{colname}";
                            // if (IsPropertyKey(column))
                            //     svalues += $"{comma}@Id";
                            // else
                            svalues += $"{comma}@{colname}";
                            comma = ", ";
                        }
                    }
                }
            });

            // var connClose = (conn.State == ConnectionState.Closed);

            var sSql = @$"Insert Into {GetTableName(typeof(T))} ({scolumns}) values ({svalues})";
            // if (connClose) conn.Close();
            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);
        }

        public static Task<int> UpdateAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var scolumns = "";
            var comma = "";
            var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.Where(IsUpdateable).ToList().ForEach(column =>
            {

                var colname = GetColumnName(column);
                if (!colname.ToUpper().Equals("Notifications".ToUpper()))
                {
                    if (!colname.ToUpper().Equals("Valid".ToUpper()))
                    {
                        if (!colname.ToUpper().Equals("Invalid".ToUpper()))
                        {
                            if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                            {
                                // swhere = $"{colname} = @Id";
                                swhere = $"{colname} = @{colname}";
                            }
                            else
                            {
                                scolumns += $"{comma}{colname} = @{colname}";
                                comma = ", ";
                            }
                        }
                    }
                }
            });

            var sSql = $"Update {GetTableName(typeof(T))} Set {scolumns} Where {swhere}";

            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);

        }

        public static Task<int> DeleteAsync<T>(this IDbConnection conn, T tobject, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            var swhere = "";
            var columns = GetPropertyNames(typeof(T));
            columns.ToList().ForEach(column =>
            {
                var colname = GetColumnName(column);
                if (colname.ToUpper() == "ID" || colname.ToUpper() == $"ID{GetTableName(typeof(T))}" || colname.ToUpper() == $"{GetTableName(typeof(T))}ID" || IsPropertyKey(column))
                {
                    swhere = $"{colname} = @{colname}";
                }
            });

            var sSql = $"Delete From {GetTableName(typeof(T))}  Where {swhere}";

            return conn.ExecuteAsync(sSql, tobject, transaction, commandTimeout);

        }

        private static IEnumerable<PropertyInfo> GetPropertyNames(Type tclass)
        {
            // var tclass = oclass.GetType();

            var propertys = tclass.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                            .Where(IsWriteable);

            return propertys;
        }


        private static string GetTableName(Type tclass)
        {
            // var tclass = oclass.GetType();

            string attname = tclass.GetCustomAttribute<TableAttribute>(false)?.Name ?? (tclass.GetCustomAttributes(false).FirstOrDefault(a => a.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (attname != null)
                return attname;

            return tclass.Name;
        }

        private static string GetColumnName(PropertyInfo tcolumn)
        {

            string attname = tcolumn.GetCustomAttribute<ColumnAttribute>(false)?.Name ?? (tcolumn.GetCustomAttributes(false).FirstOrDefault(a => a.GetType().Name == "ColumnAttribute") as dynamic)?.Name;
            if (attname != null)
                return attname;

            return tcolumn.Name;
        }

        private static bool IsPropertyKey(PropertyInfo tcolumn)
        {
            var attname = tcolumn.GetCustomAttribute<KeyAttribute>(false);

            return (attname != null);
        }

        private static bool IsWriteable(PropertyInfo inf)
        {
            var attributes = inf.GetCustomAttributes(typeof(WritePropertyAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WritePropertyAttribute)attributes[0];
            return writeAttribute.WriteProperty;
        }

        private static bool IsUpdateable(PropertyInfo inf)
        {
            var attibutes = inf.GetCustomAttributes(typeof(UpdatePropertyAttribute), false).AsList();
            if (attibutes.Count <= 0) return true;
            var updateAttribute = (UpdatePropertyAttribute)attibutes[0];

            return updateAttribute.UpdateProperty;
        }

        private static bool IsInsertable(PropertyInfo inf)
        {
            var attibutes = inf.GetCustomAttributes(typeof(InsertPropertyAttribute), false).AsList();
            if (attibutes.Count <= 0) return true;
            var insertAttibute = (InsertPropertyAttribute)attibutes[0];

            return insertAttibute.InsertProperty;
        }

    }

    static class ExtensionsString
    {
        public static bool IsEmpty(this string value)
        {
            return (string.IsNullOrEmpty(value));
        }
        public static string Left(this string value, int size)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            if (value.Length <= size)
            {
                return value;
            }
            return value.Substring(0, size);
        }

        public static string NumbersOnly(this string value)
        {
            if (value.IsEmpty())
            {
                return "0";
            }
            var aux = "";
            var arr = value.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!char.IsDigit(arr[i]))
                {
                    continue;
                }
                aux += arr[i];
            }
            return (aux.IsEmpty() ? "0" : aux);
        }

        public static bool IsNotEmpty(this string value)
        {
            return (!value.IsEmpty());
        }

        public static string Clear(this string value)
        {
            return string.Empty;
        }
        public static string Right(this string value, int size)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            if (value.Length <= size)
            {
                return value;
            }
            return value.Substring(value.Length - size);
        }

        public static string Right(this string value, int start, int size)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            if (value.Length <= size)
            {
                return value;
            }
            return value.Substring(start, value.Length - size);
        }
        public static string Left(this string value, int start, int size)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            if (value.Length <= size)
            {
                return value;
            }
            return value.Substring(start, size);
        }
        /// <summary>
        /// Converter String em Inteiro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Inteiro</returns>
        public static int ToInt(this string value)
        {
            var retorno = value.NumbersOnly();
            if (retorno.IsEmpty())
            {
                return 0;
            }
            return Convert.ToInt32(retorno);
        }

        public static bool IsNumeric(this string value)
        {
            return double.TryParse(value, out double retorno);
        }

        public static bool IsNotEMail(this string value)
        {
            // var regExp = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
            // var rg = new Regex(regExp);
            // if (rg.IsMatch(value))
            //     return true;
            try
            {
                var mail = new System.Net.Mail.MailAddress(value);
                return (mail.Address != value);
            }
            catch
            {
                return true;
            }
        }

        public static short ToShort(this string value)
        {
            var retorno = value.NumbersOnly();
            if (retorno.IsEmpty())
                return 0;

            return Convert.ToInt16(retorno);
        }

        /// <summary>
        /// Converter String em Inteiro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Inteiro</returns>
        public static long ToLong(this string value)
        {
            var retorno = value.NumbersOnly();
            if (retorno.IsEmpty())
            {
                return 0;
            }
            return Convert.ToInt64(retorno);
        }

        public static string RemoveAccentuation(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(value));
        }

        public static string RemoverAcentuacaoToUpper(this string value)
        {
            var retorno = RemoveAccentuation(value);
            if (string.IsNullOrEmpty(retorno))
                return retorno;
            return retorno.ToUpper();
        }

        public static bool IsDateTime(this string value)
        {
            return DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out DateTime aux);
        }
        /// <summary>
        /// Converter String em DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DateTime</returns>
        public static DateTime? ToDateTimeOrNull(this string value)
        {
            DateTime retorno = DateTime.Now;
            if (value.IsEmpty())
                return null;
            DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out retorno);
            return retorno;
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime retorno = DateTime.Now;
            DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out retorno);
            return retorno;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal aux = 0m;
            if (!decimal.TryParse(value.Replace(".", ","), NumberStyles.Any, CultureInfo.CreateSpecificCulture("pt-BR"), out aux))
                return 0m;
            return aux;
        }
        public static double ToDouble(this string value)
        {
            double aux = 0;
            if (!double.TryParse(value.Replace(".", ","), NumberStyles.Any, CultureInfo.CreateSpecificCulture("pt-BR"), out aux))
                return 0;
            return aux;
        }

        public static string ToSha512(this string value, string key = null)
        {

            if (value.IsEmpty())
                return null;

            using (SHA512 crypt = SHA512.Create())
            {
                var mystring = $"{value}{key.ObjectToString().Trim()}";
                var hash = string.Empty;
                byte[] mybytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(mystring));
                for (int i = 0; i < mybytes.Length; i++)
                {
                    hash += mybytes[i].ToString("x2");
                }

                return hash;
            }
        }

        public static string ToSha256(this string value, string key = null)
        {
            if (value.IsEmpty())
                return null;

            using (SHA256 crypt = SHA256.Create())
            {
                var mystring = $"{value}{key.ToString().Trim()}";
                var hash = string.Empty;
                byte[] mybytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(mystring));
                for (int i = 0; i < mybytes.Length; i++)
                {
                    hash += mybytes[i].ToString("x2");
                }

                return hash;
            }
        }

        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

    }

    static class ExtensionsObject
    {
        public static bool IsNull(this object value)
        {
            return (value == null);
        }

        public static bool IsNotNull(this object value)
        {
            return (value != null);
        }

        public static string ObjectToString(this object value)
        {
            if (value.IsNull())
                return string.Empty;

            return value.ToString();
        }


        public static string ToJson<T>(this T value) where T : class => JsonSerializer.Serialize<T>(value);

        public static T Load<T>(this T innerobject, object loadedobject) where T : class
        {
            if (loadedobject.IsNull())
                return innerobject;

            var innertype = innerobject.GetType();
            var loadedtype = loadedobject.GetType();

            var innerprops = innertype.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var loadedprops = loadedtype.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Parallel.ForEach(innerprops, innerprop =>
             {
                 Parallel.ForEach(loadedprops, loadedprop =>
                 {
                     var custattibs = innerprop.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
                     if (custattibs.IsNull())
                     {
                         if (innerprop.Name == loadedprop.Name)
                         {
                             var value = loadedprop.GetValue(loadedobject);
                             var innervalue = innerprop.GetValue(innerobject);
                             if (innerprop.PropertyType.ObjectToString().ToLower().Contains("ayugate") && innerprop.PropertyType.BaseType == typeof(System.Object))
                             {
                                 innervalue.Load(value);
                                 innerprop.SetValue(innerobject, innervalue);
                             }
                             else
                             if (value.IsNotNull() && innervalue.ObjectToString() != value.ObjectToString())
                             {
                                 if ((loadedprop.PropertyType == typeof(DateTime) || loadedprop.PropertyType == typeof(DateTime?)) && innerprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = (DateTime)value;
                                     innerprop.SetValue(innerobject, valueAux.ToStringDateBrazilian());
                                 }
                                 else if ((innerprop.PropertyType == typeof(DateTime) || innerprop.PropertyType == typeof(DateTime?)) && loadedprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = value.ObjectToString().ToDateTime();
                                     innerprop.SetValue(innerobject, valueAux);
                                 }
                                 else
                                     innerprop.SetValue(innerobject, value);
                             }
                         }
                         if (innerprop.Name == loadedprop.Name)
                         {
                             var value = loadedprop.GetValue(loadedobject);
                             var innervalue = innerprop.GetValue(innerobject);
                             if (innerprop.PropertyType.ObjectToString().ToLower().Contains("intelisystem") && innerprop.PropertyType.BaseType == typeof(System.Object))
                             {
                                 innervalue.Load(value);
                                 innerprop.SetValue(innerobject, innervalue);
                             }
                             else
                             if (value.IsNotNull() && innervalue.ObjectToString() != value.ObjectToString())
                             {
                                 if ((loadedprop.PropertyType == typeof(DateTime) || loadedprop.PropertyType == typeof(DateTime?)) && innerprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = (DateTime)value;
                                     innerprop.SetValue(innerobject, valueAux.ToStringDateBrazilian());
                                 }
                                 else if ((innerprop.PropertyType == typeof(DateTime) || innerprop.PropertyType == typeof(DateTime?)) && loadedprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = value.ObjectToString().ToDateTime();
                                     innerprop.SetValue(innerobject, valueAux);
                                 }
                                 else
                                     innerprop.SetValue(innerobject, value);
                             }
                         }
                     }
                 });
             });

            return innerobject;
        }
    }

    static class ExtensionsDateTime
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


        public static string ToStringDateTimeBrazilian(this DateTime value)
        {
            return ToDateTime(value);
        }
        public static string ToStringDateBrazilian(this DateTime value)
        {
            return ToDate(value);
        }

        public static string ToStringDateBrazilian(this DateTime? value)
        {
            if (value == null)
                return "__/__/____";

            return ToDate((DateTime)value);
        }

        private static string ToDate(DateTime value)
        {
            var day = value.Day.ZeroLeft(2);
            var month = value.Month.ZeroLeft(2);
            var year = value.Year.ZeroLeft(4);
            return $"{day}/{month}/{year}";
        }

        private static string ToDateTime(DateTime value)
        {
            var data = ToDate(value);
            return $"{data} {value.ToString("HH:mm:ss")}";
        }
    }

    static class ExtensionsNumber
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
        public static decimal Round(this decimal value, int decimalplaces)
        {
            value = Math.Round(value, decimalplaces);
            return value;
        }

        public static string FormatDecimal(this decimal value, int decimalplaces)
        {
            var retorno = string.Format("{0:0." + new string('0', decimalplaces) + "}", value);
            return retorno;
        }

        public static string ZeroLeft(this object value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static string ZeroLeft(this int value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }
        public static string ZeroLeft(this double value, int size)
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
    }
}