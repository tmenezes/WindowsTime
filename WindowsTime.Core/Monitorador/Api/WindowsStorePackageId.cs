using System;
using System.IO;
using WindowsTime.Core.Monitorador.Api.Structs;
using WindowsTime.Core.Monitorador.Helpers;

namespace WindowsTime.Core.Monitorador.Api
{
    public class WindowsStorePackageId
    {
        public int Reserved { get; private set; }
        public int ProcessorArchitecture { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; set; }
        public string Publisher { get; private set; }
        public string ResourceId { get; private set; }
        public string PublisherId { get; private set; }

        public int Version { get; private set; }
        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Build { get; private set; }
        public int Revision { get; private set; }
        public string PackageVersion { get { return string.Format("{0}.{1}.{2}.{3}", Major, Minor, Build, Revision); } }

        public string InstalledFolder { get; set; }
        public string ResourcesPriFilePath { get; set; }

        public WindowsStorePackageId(PACKAGE_ID packageId, string fullname, string path)
        {
            Reserved = (int)packageId.reserved;
            ProcessorArchitecture = (int)packageId.processorArchitecture;

            Name = MarshalHelper.SafePtrToStringUni(packageId.name);
            FullName = fullname;
            Publisher = MarshalHelper.SafePtrToStringUni(packageId.publisher);
            ResourceId = MarshalHelper.SafePtrToStringUni(packageId.resourceId);
            PublisherId = MarshalHelper.SafePtrToStringUni(packageId.publisherId);

            Version = (int)packageId.version.Version;
            Major = (int)packageId.version.Major;
            Minor = (int)packageId.version.Minor;
            Build = (int)packageId.version.Build;
            Revision = (int)packageId.version.Revision;

            InstalledFolder = path;
            ResourcesPriFilePath = Path.Combine(InstalledFolder, "resources.pri");
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, FullName: {1}, Version: {2}, Publisher: {3}, ResourceId: {4}, PublisherId: {5}",
                Name, FullName, PackageVersion, Publisher, ResourceId, PublisherId);
        }
    }
}
