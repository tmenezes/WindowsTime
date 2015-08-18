using System;

namespace WindowsTime.ImportExport
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportExportConfigurationBaseAttribute : Attribute
    {
        public string Description { get; set; }
        public string Format { get; set; }
        public bool DoTrim { get; set; }
        public int DecimalPlaces { get; set; }
        public bool HasDecimalDelimiter { get; set; }
        public int Position { get; set; }
        public string Culture { get; set; }

        public ImportExportConfigurationBaseAttribute()
        {
            this.DoTrim = true;
            this.DecimalPlaces = 2;
        }
        public ImportExportConfigurationBaseAttribute(string description)
            : this()
        {
            this.Description = description;
        }

        public virtual bool Validate()
        {
            return true;
        }

        public virtual ImportExportConfigurationBaseAttribute BuildAutomaticPosition()
        {
            return this;
        }
    }
}