using System.IO;
using WindowsTime.Core.ImportExport.CsvFile;

namespace WindowsTime.Core.ImportExport
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