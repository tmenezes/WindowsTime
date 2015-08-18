using System;
using System.Collections.Generic;

namespace WindowsTime.ImportExport
{
    public interface IImporter : IDisposable
    {
        IEnumerable<T> DoImport<T>();
        IEnumerable<T> GetSection<T>(string sectionKey);
    }
}
