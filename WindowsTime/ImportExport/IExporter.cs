using System.Collections.Generic;

namespace WindowsTime.ImportExport
{
    public interface IExporter
    {
        void DoExport<T>(IEnumerable<T> exportObjects);
        void AddToExport<T>(IEnumerable<T> exportObjects);
        void Save();
    }
}
