using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;

namespace InteliSystem.Utils.Extensions
{
    internal class SecurityKey
    {
        private const string _privateKey = "8573ddfcb5d14a2ba9061b0dcb0ce462";

        public static string PrivateKey => _privateKey;
    }
    public static class StringExtensions
    {


        public static bool IsEmpty(this string? value)
        {
            return (string.IsNullOrEmpty(value));
        }
        public static string Left(this string? value, int size)
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

        public static string NumbersOnly(this string? value)
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

        public static bool IsNotEmpty(this string? value)
        {
            return (!value.IsEmpty());
        }

        public static T JsonToObject<T>(this string? value, bool ignoreNullValues = false) where T : class
            => JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                IgnoreNullValues = ignoreNullValues
            });

        public static string Clear(this string? value)
        {
            return string.Empty;
        }
        public static string Right(this string? value, int size)
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

        public static string Right(this string? value, int start, int size)
        {

            if (size == value.Length && start > 0)
                size = value.Length - start;

            if (value.IsEmpty())
                return string.Empty;

            if (value.Length <= size)
                return value;
            return value.Substring(start, size);
        }
        public static string Left(this string? value, int start, int size)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            try
            {
                return value.Substring(start, size);
            }
            catch
            {
                return value.Substring(start, size - start);
            }
        }
        /// <summary>
        /// Converter String em Inteiro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Inteiro</returns>
        public static int ToInt(this string? value)
        {
            var retorno = value.NumbersOnly();
            if (retorno.IsEmpty())
            {
                return 0;
            }
            return Convert.ToInt32(retorno);
        }

        public static bool IsNumeric(this string? value)
        {
            return double.TryParse(value, out double retorno);
        }

        public static bool IsNotEMail(this string? value)
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

        public static bool IsEMail(this string? value)
        {
            return !(value.IsNotEMail());
        }

        public static short ToShort(this string? value)
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
        public static long ToLong(this string? value)
        {
            var retorno = value.NumbersOnly();
            if (retorno.IsEmpty())
            {
                return 0;
            }
            return Convert.ToInt64(retorno);
        }

        public static string RemoveAccentuation(this string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(value));
        }

        public static string RemoveAccentuationToUpper(this string? value)
        {
            var retorno = RemoveAccentuation(value);
            if (string.IsNullOrEmpty(retorno))
                return retorno;
            return retorno.ToUpper();
        }

        public static bool IsDateTime(this string? value)
        {
            return DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out DateTime aux);
        }
        /// <summary>
        /// Converter String em DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns>DateTime</returns>
        public static DateTime? ToDateTimeOrNull(this string? value)
        {
            DateTime retorno = DateTime.Now;
            if (value.IsEmpty())
                return null;
            DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out retorno);
            return retorno;
        }

        public static DateTime ToDateTime(this string? value)
        {
            DateTime retorno = DateTime.Now;
            DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out retorno);
            return retorno;
        }

        public static decimal ToDecimal(this string? value)
        {
            decimal aux = 0m;
            if (!decimal.TryParse(value.Replace(".", ","), NumberStyles.Any, CultureInfo.CreateSpecificCulture("pt-BR"), out aux))
                return 0m;
            return aux;
        }
        public static double ToDouble(this string? value)
        {
            double aux = 0d;
            if (!double.TryParse(value.Replace(".", ","), NumberStyles.Any, CultureInfo.CreateSpecificCulture("pt-BR"), out aux))
                return 0d;
            return aux;
        }

        public static string ToSha512(this string? value, string key = null)
        {

            if (value.IsEmpty())
                return null;

            using (SHA512 crypt = SHA512.Create())
            {
                var mystring = $"{value}{key.ObjectToString().Trim()}";
                var hash = string.Empty;
                byte[] mybytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(mystring));
                crypt.Clear();
                hash = mybytes.ArrayBytesToString();

                return hash;
            }
        }

        public static string ToSha256(this string? value, string key = null)
        {
            if (value.IsEmpty())
                return null;

            using (SHA256 crypt = SHA256.Create())
            {
                var mystring = $"{value}{key.ObjectToString().Trim()}";
                var hash = string.Empty;
                byte[] mybytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(mystring));
                crypt.Clear();
                hash = mybytes.ArrayBytesToString();

                return hash;
            }
        }

        public static string ArrayBytesToString(this byte[] value)
        {
            var hash = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                hash += value[i].ToString("x2");
            }
            return hash;
        }

        public static string ToBase64(this string? value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }

        public static byte[] BinaryToByteArray(this string? value)
        {
            var numbytes = (int)Math.Ceiling(value.Length / 8m);
            var bytes = new byte[numbytes];
            var chunksize = 8;
            for (int i = 1; i <= numbytes; i++)
            {
                var startIndex = value.Length - 8 * i;
                if (startIndex < 0)
                {
                    chunksize = 8 + startIndex;
                    startIndex = 0;
                }

                bytes[numbytes - i] = Convert.ToByte(value.Substring(startIndex, chunksize), 2);
            }
            return bytes;
        }

        public static string ToMD5(this string? value)
        {

            using (var md5 = MD5.Create())
            {
                byte[] mybytes = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
                md5.Clear();
                return mybytes.ArrayBytesToString();
            }
        }

        public static byte[] ToMD5Bytes(this string? value)
        {

            using (var md5 = MD5.Create())
            {
                byte[] mybytes = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));

                return mybytes;
            }
        }

        public static string ToEncrypt(this string? value, string key = "")
        {
            if (key.IsEmpty())
                key = SecurityKey.PrivateKey;

            using (var tresDes = TripleDES.Create())
            {
                tresDes.Mode = CipherMode.ECB;
                tresDes.Padding = PaddingMode.PKCS7;
                tresDes.Key = key.ToMD5Bytes();

                byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

                var transform = tresDes.CreateEncryptor();
                byte[] arrEncript = transform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
                tresDes.Clear();

                return Convert.ToBase64String(arrEncript, 0, arrEncript.Length);

            }

        }

        public static string ToDecrypt(this string? value, string key = "")
        {
            if (key.IsEmpty())
                key = SecurityKey.PrivateKey;

            using (var tresDes = TripleDES.Create())
            {
                tresDes.Mode = CipherMode.ECB;
                tresDes.Padding = PaddingMode.PKCS7;
                tresDes.Key = key.ToMD5Bytes();

                byte[] valueBytes = Convert.FromBase64String(value);

                var transform = tresDes.CreateDecryptor();
                byte[] arrDencrypt = transform.TransformFinalBlock(valueBytes, 0, valueBytes.Length);
                tresDes.Clear();

                return UTF8Encoding.UTF8.GetString(arrDencrypt);

            }

        }
    }
}