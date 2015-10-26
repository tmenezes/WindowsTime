using System;

namespace WindowsTime.Core.ImportExport.CsvFile
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvFileConfigurationAttribute : ImportExportConfigurationBaseAttribute
    {
        public string CsvDelimiter { get; set; }

        public CsvFileConfigurationAttribute()
            : base()
        {
            this.CsvDelimiter = ";";
            this.DoTrim = false;
            this.Position = 1;
        }

        public CsvFileConfigurationAttribute(string description)
            : this()
        {
            this.Description = description;
        }
    }
}
