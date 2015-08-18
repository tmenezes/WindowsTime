using System.IO;
using WindowsTime.ImportExport.CsvFile;

namespace WindowsTime.ImportExport
{
    public static class ImportFile
    {
        public static IImporter GetImporter(ImportExportTypeEnum importExportType, string filename)
        {
            return new CsvFileImporter(filename);
        }

        public static IImporter GetImporter(ImportExportTypeEnum importExportType, Stream stream)
        {
            return new CsvFileImporter(stream);
        }
    }
}