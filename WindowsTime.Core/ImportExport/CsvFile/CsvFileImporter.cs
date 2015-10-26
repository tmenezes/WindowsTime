using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WindowsTime.Core.ImportExport.CsvFile
{
    public class CsvFileImporter : IImporter
    {
        // Property
        private IList<PropertyInfo> _propertiesToImport = null;
        private string _typeNameOfLoadedProperties = string.Empty;

        private string[] FileLines { get; set; }


        // Contructor
        internal CsvFileImporter(string filename)
        {
            this.Load(filename);
        }
        internal CsvFileImporter(Stream stream)
        {
            this.Load(stream);
        }


        // publico
        public IEnumerable<T> DoImport<T>()
        {
            IEnumerable<T> listDTO = this.DoImportLines<T>(this.FileLines).ToList();

            return listDTO;
        }
        private IEnumerable<T> DoImportLines<T>(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                var csvObject = (T)Activator.CreateInstance(typeof(T));

                this.FillCsvFileObject(csvObject, line);

                yield return csvObject;
            }
        }

        public IEnumerable<T> GetSection<T>(string sectionKey)
        {
            IEnumerable<string> sectionLines = this.GetLinesBySectionKey(sectionKey);
            IEnumerable<T> listDTO = this.DoImportLines<T>(sectionLines).ToList();

            return listDTO;
        }

        public void Dispose()
        {
            _propertiesToImport.Clear();
            _propertiesToImport = null;
            FileLines = new string[] { };
        }


        // privados
        private void Load(string filename)
        {
            try
            {
                this.FileLines = this.GetFileLines(filename);
            }
            catch (Exception)
            {
                throw new ImportExportException("File not found or could not be loaded!");
            }
        }
        private void Load(Stream stream)
        {
            try
            {
                this.FileLines = this.GetFileLines(stream);
            }
            catch (System.Exception)
            {
                throw new ImportExportException("Stream could not be loaded!");
            }
        }
        private string[] GetFileLines(string filename)
        {
            string fileContent = ImportExportHelper.GetFileContent(filename);
            string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return lines;
        }
        private string[] GetFileLines(Stream stream)
        {
            string fileContent = ImportExportHelper.GetFileContent(stream);
            string[] lines = fileContent.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            return lines;
        }
        private IEnumerable<string> GetLinesBySectionKey(string sectionKey)
        {
            var sectionLines = this.FileLines.Where(s => s.StartsWith(sectionKey)).ToList();
            return sectionLines;
        }

        private void FillCsvFileObject<T>(T csvFileObject, string line)
        {
            var properties = this.GetPropertiesToImport(csvFileObject);
            bool noPropertiesFound = properties == null || properties.Count == 0;
            if (noPropertiesFound)
                throw new ImportExportException("No properties mapped with 'CsvFileConfigurationAttribute'.");

            foreach (PropertyInfo property in properties)
            {
                object value = this.GetPropertyValue(line, property);
                this.FillPropertyValue(csvFileObject, property, value);
            }
        }
        private object GetPropertyValue(string line, PropertyInfo property)
        {
            CsvFileConfigurationAttribute attribute = null;
            string value = string.Empty;
            try
            {
                attribute = ImportExportHelper.GetPropertyAttribute<CsvFileConfigurationAttribute>(property);
                if (!attribute.Validate())
                    throw new ImportExportException("The csv file configuration is wrong.");

                string[] values = line.Split(new[] { attribute.CsvDelimiter }, StringSplitOptions.None);
                value = values[attribute.Position - 1];
                if (attribute.DoTrim)
                    value = value.Trim();

                object propertyValue = ImportExportHelper.GetValueFromTextValue(property, value, attribute);
                return propertyValue;
            }
            catch (Exception ex)
            {
                string description = (attribute != null) ? string.Format("\n{0}", attribute.Description) : "";
                string error = string.Format("The csv file configuration is wrong. Property: '{0}' | Value: '{1}'{2}", property.Name, value, description);
                throw new ImportExportException(error, ex);
            }
        }
        private void FillPropertyValue<T>(T textFileObject, PropertyInfo property, object value)
        {
            try
            {
                property.SetValue(textFileObject, value, null);
            }
            catch (Exception ex)
            {
                string error = string.Format("The csv file configuration is wrong. Property: '{0}' | Value: '{1}'", property.Name, value);
                throw new ImportExportException(error, ex);
            }
        }

        private IList<PropertyInfo> GetPropertiesToImport<T>(T objectForExport)
        {
            if (this._propertiesToImport == null)
                this._propertiesToImport = ImportExportHelper.GetProperties<CsvFileConfigurationAttribute>(objectForExport.GetType());

            bool typeOfFileImportAreDiferents = !this._typeNameOfLoadedProperties.Equals(typeof(T).FullName);
            bool forceLoadPropertiesToImport = typeOfFileImportAreDiferents || string.IsNullOrEmpty(this._typeNameOfLoadedProperties);
            if (forceLoadPropertiesToImport)
                this._propertiesToImport = ImportExportHelper.GetProperties<CsvFileConfigurationAttribute>(objectForExport.GetType());


            this._typeNameOfLoadedProperties = typeof(T).FullName;
            return this._propertiesToImport;
        }
    }
}
