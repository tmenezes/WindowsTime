using System;
using System.Runtime.InteropServices;
using WindowsTime.Monitorador.Api.Helpers;
using WindowsTime.Monitorador.Api.Structs;

namespace WindowsTime.Monitorador.Api
{
    public class WindowsStorePackageId
    {
        public int Reserved { get; private set; }
        public int ProcessorArchitecture { get; private set; }
        public string Name { get; private set; }
        public string Publisher { get; private set; }
        public string ResourceId { get; private set; }
        public string PublisherId { get; private set; }

        public int Version;
        public int Major;
        public int Minor;
        public int Build;
        public int Revision;
        public string PackageVersion { get { return string.Format("{0}.{1}.{2}.{3}", Major, Minor, Build, Revision); } }

        public WindowsStorePackageId(PACKAGE_ID packageId)
        {
            Reserved = (int)packageId.reserved;
            ProcessorArchitecture = (int)packageId.processorArchitecture;

            Name = MarshalHelper.SafePtrToStringUni(packageId.name);
            Publisher = MarshalHelper.SafePtrToStringUni(packageId.publisher);
            ResourceId = MarshalHelper.SafePtrToStringUni(packageId.resourceId);
            PublisherId = MarshalHelper.SafePtrToStringUni(packageId.publisherId);

            Version = (int)packageId.version.Version;
            Major = (int)packageId.version.Major;
            Minor = (int)packageId.version.Minor;
            Build = (int)packageId.version.Build;
            Revision = (int)packageId.version.Revision;
        }
    }
}
