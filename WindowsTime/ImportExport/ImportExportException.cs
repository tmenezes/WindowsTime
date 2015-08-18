namespace WindowsTime.ImportExport
{
    public class ImportExportException : System.Exception
    {
        public ImportExportException(string message)
            : base(message)
        {
        }

        public ImportExportException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
