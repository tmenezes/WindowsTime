using System;
using System.Runtime.InteropServices;

namespace WindowsTime.Monitorador.Api.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PACKAGE_ID
    {
        internal uint reserved;
        internal uint processorArchitecture;
        internal PACKAGE_VERSION version;
        internal IntPtr name;
        internal IntPtr publisher;
        internal IntPtr resourceId;
        internal IntPtr publisherId;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct PACKAGE_VERSION
    {
        [FieldOffset(0)]
        public UInt64 Version;
        [FieldOffset(0)]
        public ushort Revision;
        [FieldOffset(2)]
        public ushort Build;
        [FieldOffset(4)]
        public ushort Minor;
        [FieldOffset(6)]
        public ushort Major;
    }
}
