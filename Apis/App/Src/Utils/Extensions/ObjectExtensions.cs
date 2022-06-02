using System;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Extensions
{
    public static class ObjectExtensions
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


        public static string ToJson<T>(this T value, bool ignoreNullValues = true) where T : class => JsonSerializer.Serialize<T>(value, new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            IgnoreNullValues = ignoreNullValues
        });

        public static T Load<T>(this T innerobject, object loadedobject) where T : class
        {

            if (innerobject.IsNull() || loadedobject.IsNull())
            {
                return null;
            }
            var thisType = innerobject.GetType();
            var loadedobjectType = loadedobject.GetType();

            var thisProp = thisType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var loadedobjectProp = loadedobjectType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            thisProp.ToList().ForEach(prop =>
            {
                loadedobjectProp.ToList().ForEach(cprop =>
                {
                    var custattibs = prop.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
                    if (custattibs.IsNull())
                    {
                        if (prop.Name == cprop.Name)//if (prop.Name == cprop.Name && prop.Name.ToLower() != "password")
                        {
                            if (prop.PropertyType.ToString().ToUpper().Contains("InteliSystem") && prop.PropertyType.BaseType != typeof(System.Enum))
                            {
                                var aux1 = prop.GetValue(innerobject);
                                var aux2 = cprop.GetValue(loadedobject);
                                aux1.Load(aux2);
                                prop.SetValue(innerobject, aux1);
                            }
                            else
                            {
                                var valor = cprop.GetValue(loadedobject);
                                var innervalue = prop.GetValue(innerobject);
                                if (valor.IsNotNull() && (valor.ObjectToString() != innervalue.ObjectToString()))
                                {

                                    if (prop.PropertyType == typeof(string))
                                        if (prop.PropertyType == typeof(string) && cprop.PropertyType == typeof(decimal))
                                            prop.SetValue(innerobject, valor.ObjectToString().ToDecimal().FormatDecimal(2));
                                        else
                                            prop.SetValue(innerobject, valor.ObjectToString().Trim());
                                    else
                                        prop.SetValue(innerobject, valor);
                                }
                            }

                        }
                    }
                });
            });

            return innerobject;
        }
    }
}