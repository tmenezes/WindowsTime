using WindowsTime.Core.ImportExport.CsvFile;

namespace WindowsTime.Core.ImportExport
{
    public static class ExportFile
    {
        public static IExporter GetExporter(ImportExportTypeEnum importExportType, string filename)
        {
            return new CsvFileExporter(filename);
        }
    }
}
