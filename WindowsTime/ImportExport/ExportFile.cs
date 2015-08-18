using WindowsTime.ImportExport.CsvFile;

namespace WindowsTime.ImportExport
{
    public static class ExportFile
    {
        public static IExporter GetExporter(ImportExportTypeEnum importExportType, string filename)
        {
            return new CsvFileExporter(filename);
        }
    }
}
