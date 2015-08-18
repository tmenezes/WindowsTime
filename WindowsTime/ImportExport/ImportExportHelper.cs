using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WindowsTime.ImportExport
{
    internal class ImportExportHelper
    {
        internal static IList<PropertyInfo> GetProperties<TAttribute>(Type type) where TAttribute : ImportExportConfigurationBaseAttribute
        {
            var listProperties = type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                     .Where(p => p.GetCustomAttributes(typeof(TAttribute), true).Length > 0)
                                     .OrderBy(p => ((TAttribute)p.GetCustomAttributes(typeof(TAttribute), true)[0]).BuildAutomaticPosition().Position)
                                     .ToList();
            return listProperties;
        }
        internal static T GetPropertyAttribute<T>(PropertyInfo property)
        {
            object[] columnAttributes = property.GetCustomAttributes(typeof(T), true);

            if (columnAttributes.Length == 0)
                throw new ImportExportException($"A propriedade '{property.Name}' não contém um atributo de mapeamento DataPropertyInfo.");
            if (columnAttributes.Length > 1)
                throw new ImportExportException($"A propriedade '{property.Name}' não pode conter mais de um atributo de mapeamento DataPropertyInfo.");

            return (T)columnAttributes[0];
        }
        internal static T GetAttribute<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttributes<T>().FirstOrDefault();
        }

        internal static string GetFileContent(string filename)
        {
            if (!File.Exists(filename))
                throw new ImportExportException("File not Found");

            StreamReader reader = new StreamReader(filename, Encoding.Default);
            string fileContent = reader.ReadToEnd();
            reader.Dispose();

            return fileContent;
        }
        internal static string GetFileContent(Stream stream)
        {
            if (stream == null)
                throw new ImportExportException("Stream is Null");

            StreamReader reader = new StreamReader(stream, Encoding.Default);
            string fileContent = reader.ReadToEnd();
            reader.Dispose();

            return fileContent;
        }
        internal static void SaveFileContent(string filename, string content)
        {
            using (StreamWriter writer = new StreamWriter(filename, false, Encoding.Default))
            {
                writer.Write(content);
                writer.Flush();
            }
        }
        internal static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
        }


        internal static object GetValueFromTextValue(PropertyInfo property, string textValue, ImportExportConfigurationBaseAttribute attribute)
        {
            bool valueIsString = property.PropertyType == typeof(string);
            if (valueIsString)
                return textValue;

            bool valueIsInteger = property.PropertyType == typeof(int) || property.PropertyType == typeof(int?);
            if (valueIsInteger)
                return Convert.ToInt32(textValue);

            bool valueIsDate = property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?);
            if (valueIsDate)
            {
                if (string.IsNullOrEmpty(textValue))
                    return null;

                if (!string.IsNullOrEmpty(attribute.Format))
                    return DateTime.ParseExact(textValue, attribute.Format, CultureInfo.GetCultureInfo("en-US"));

                return Convert.ToDateTime(textValue);
            }

            bool valueIsRealNumber = property.PropertyType == typeof(double) || property.PropertyType == typeof(double?);
            if (valueIsRealNumber)
            {
                if (!attribute.HasDecimalDelimiter)
                    textValue = textValue.Insert(textValue.Length - attribute.DecimalPlaces, ".");

                return Convert.ToDouble(textValue, CultureInfo.GetCultureInfo("en-US"));
            }

            return textValue;
        }
        internal static object GetValueFromProperty(PropertyInfo property, object objectPropertyOwner)
        {
            object value = property.GetValue(objectPropertyOwner, null);
            return value;
        }
        internal static string GetValueAndApplyAttributeConfig(PropertyInfo property, ImportExportConfigurationBaseAttribute attribute, object objectPropertyOwner)
        {
            object value = property.GetValue(objectPropertyOwner, null);
            if (value == null)
                return null;

            bool valueIsString = property.PropertyType == typeof(string);
            if (valueIsString)
                return (string)value;

            bool valueIsInteger = property.PropertyType == typeof(int) || property.PropertyType == typeof(int?);
            if (valueIsInteger)
                return ((int)value).ToString();

            bool valueIsDate = property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?);
            if (valueIsDate)
                return ApplyFormatAndCultue((DateTime)value, attribute);

            bool valueIsRealNumber = IsRealNumberProperty(property);
            if (valueIsRealNumber)
            {
                decimal valueAsDecimal = Math.Round(Convert.ToDecimal(value), attribute.DecimalPlaces);
                string valueAsString = ApplyFormatAndCultue(valueAsDecimal, attribute);

                if (!attribute.HasDecimalDelimiter)
                    valueAsString = valueAsString.Replace(".", "").Replace(",", "");

                return valueAsString;
            }

            return null;
        }

        internal static string ApplyFormatAndCultue(object value, PropertyInfo property, ImportExportConfigurationBaseAttribute attribute)
        {
            if (value == null)
                return null;

            if (IsIntegerProperty(property))
            {
                return ApplyFormatAndCultue(Convert.ToInt64(value), attribute);
            }

            if (IsRealNumberProperty(property))
            {
                return ApplyFormatAndCultue(Convert.ToDecimal(value), attribute);
            }

            if (IsDateTimeValue(property))
            {
                return ApplyFormatAndCultue(Convert.ToDateTime(value), attribute);
            }

            return value.ToString();
        }
        internal static string ApplyFormatAndCultue(decimal value, ImportExportConfigurationBaseAttribute attribute)
        {
            string valueAsString = value.ToString();
            bool hasFormat = !string.IsNullOrEmpty(attribute.Format);
            bool hasCulture = !string.IsNullOrEmpty(attribute.Culture);

            if (hasFormat && hasCulture)
            {
                valueAsString = value.ToString(attribute.Format, new CultureInfo(attribute.Culture));
            }
            else if (hasFormat)
            {
                valueAsString = value.ToString(attribute.Format);
            }
            else if (hasCulture)
            {
                valueAsString = value.ToString(new CultureInfo(attribute.Culture));
            }
            return valueAsString;
        }
        internal static string ApplyFormatAndCultue(long value, ImportExportConfigurationBaseAttribute attribute)
        {
            string valueAsString = value.ToString();
            bool hasFormat = !string.IsNullOrEmpty(attribute.Format);
            bool hasCulture = !string.IsNullOrEmpty(attribute.Culture);

            if (hasFormat && hasCulture)
            {
                valueAsString = value.ToString(attribute.Format, new CultureInfo(attribute.Culture));
            }
            else if (hasFormat)
            {
                valueAsString = value.ToString(attribute.Format);
            }
            else if (hasCulture)
            {
                valueAsString = value.ToString(new CultureInfo(attribute.Culture));
            }
            return valueAsString;
        }
        internal static string ApplyFormatAndCultue(DateTime value, ImportExportConfigurationBaseAttribute attribute)
        {
            string valueAsString = value.ToString();
            bool hasFormat = !string.IsNullOrEmpty(attribute.Format);
            bool hasCulture = !string.IsNullOrEmpty(attribute.Culture);

            if (hasFormat && hasCulture)
            {
                valueAsString = value.ToString(attribute.Format, new CultureInfo(attribute.Culture));
            }
            else if (hasFormat)
            {
                valueAsString = value.ToString(attribute.Format);
            }
            else if (hasCulture)
            {
                valueAsString = value.ToString(new CultureInfo(attribute.Culture));
            }
            return valueAsString;
        }

        internal static bool IsNumericProperty(PropertyInfo property)
        {
            bool valueIsInteger = IsIntegerProperty(property);
            bool valueIsRealNumber = IsRealNumberProperty(property);

            return valueIsInteger || valueIsRealNumber;
        }
        internal static bool IsIntegerProperty(PropertyInfo property)
        {
            bool valueIsInteger = property.PropertyType == typeof(int) || property.PropertyType == typeof(int?) ||
                                  property.PropertyType == typeof(long) || property.PropertyType == typeof(long?);
            return valueIsInteger;
        }
        internal static bool IsRealNumberProperty(PropertyInfo property)
        {
            bool valueIsRealNumber = property.PropertyType == typeof(double) || property.PropertyType == typeof(double?) ||
                                     property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?) ||
                                     property.PropertyType == typeof(float) || property.PropertyType == typeof(float?);

            return valueIsRealNumber;
        }
        internal static bool IsDateTimeProperty(PropertyInfo property)
        {
            bool isDatetime = property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?);
            return isDatetime;
        }
        internal static bool IsBolleanProperty(PropertyInfo property)
        {
            bool isBollean = property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
            return isBollean;
        }

        internal static bool IsNumericValue<T>(T value)
        {
            bool valueIsInteger = IsIntegerValue(value);
            bool valueIsRealNumber = IsRealNumberValue(value);

            return valueIsInteger || valueIsRealNumber;
        }
        internal static bool IsIntegerValue<T>(T value)
        {
            var type = typeof(T);
            bool valueIsInteger = type == typeof(int) || type == typeof(int?);
            return valueIsInteger;
        }
        internal static bool IsRealNumberValue<T>(T value)
        {
            var type = typeof(T);
            bool valueIsRealNumber = type == typeof(double) || type == typeof(double?) ||
                                     type == typeof(decimal) || type == typeof(decimal?) ||
                                     type == typeof(float) || type == typeof(float?);

            return valueIsRealNumber;
        }
        internal static bool IsDateTimeValue<T>(T value)
        {
            var type = typeof(T);
            bool valueIsDateTime = type == typeof(DateTime) || type == typeof(DateTime?);
            return valueIsDateTime;
        }

        internal static void PerformanceMonitor(string monitorKey, Action monitoredAction)
        {
            var stopwatch = Stopwatch.StartNew();

            monitoredAction();

            stopwatch.Stop();
            Trace.WriteLine($"Total tempo ({monitorKey}): {stopwatch.ElapsedMilliseconds.ToString("##0,000")}");
        }
    }
}
