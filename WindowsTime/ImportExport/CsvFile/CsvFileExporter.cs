using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WindowsTime.ImportExport.CsvFile
{
    public class CsvFileExporter : IExporter
    {
        private readonly string _targetFileName;
        private IList<PropertyInfo> _propertiesToImport;
        private StringBuilder _csvFileContent = new StringBuilder();


        internal CsvFileExporter(string targetFileName)
        {
            _targetFileName = targetFileName;
        }


        public void DoExport<T>(IEnumerable<T> exportObjects)
        {
            this.AddToExport(exportObjects);
            this.Save();
        }
        public void AddToExport<T>(IEnumerable<T> exportObjects)
        {
            foreach (var objectForExport in exportObjects)
            {
                if (objectForExport == null)
                    continue;

                string csvLine = this.GenerateCsvLine(objectForExport);
                _csvFileContent.AppendLine(csvLine);
            }
        }
        public void Save()
        {
            string csvContent = _csvFileContent.ToString();
            if (!string.IsNullOrEmpty(csvContent))
                ImportExportHelper.SaveFileContent(_targetFileName, csvContent);

            _csvFileContent = new StringBuilder();
        }


        private string GenerateCsvLine<T>(T objectForExport)
        {
            var properties = this.GetPropertiesToImport(objectForExport);
            bool noPropertiesFound = properties == null || properties.Count == 0;
            if (noPropertiesFound)
                throw new ImportExportException("No properties mapped with 'CsvFileConfigurationAttribute'.");

            StringBuilder csvLine = new StringBuilder();
            foreach (PropertyInfo property in properties)
            {
                string lineColumn = this.GetPropertyValue(property, objectForExport);
                csvLine.Append(lineColumn);
            }
            return csvLine.ToString();
        }
        private string GetPropertyValue(PropertyInfo property, object objectForExport)
        {
            CsvFileConfigurationAttribute attribute = null;
            try
            {
                attribute = ImportExportHelper.GetPropertyAttribute<CsvFileConfigurationAttribute>(property);
                if (!attribute.Validate())
                    throw new System.Exception("The CSV file configuration is wrong.");


                string propertyValue = ImportExportHelper.GetValueAndApplyAttributeConfig(property, attribute, objectForExport);
                propertyValue = string.Concat(propertyValue, attribute.CsvDelimiter); //appling csv delimiter

                return propertyValue;
            }
            catch (System.Exception)
            {
                string description = (attribute != null) ? string.Format(" - {0}", attribute.Description) : "";
                string error = string.Format("The csv file configuration is wrong. Property: {0}{1}", property.Name, description);
                throw new ImportExportException(error);
            }
        }
        private IList<PropertyInfo> GetPropertiesToImport<T>(T objectForExport)
        {
            if (_propertiesToImport == null)
                _propertiesToImport = ImportExportHelper.GetProperties<CsvFileConfigurationAttribute>(objectForExport.GetType());

            return _propertiesToImport;
        }
    }
}
