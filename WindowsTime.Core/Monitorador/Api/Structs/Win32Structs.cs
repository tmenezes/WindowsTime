using System;
using System.Runtime.InteropServices;

namespace WindowsTime.Core.Monitorador.Api.Structs
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

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int iLeft;
        public int iTop;
        public int iRight;
        public int iBottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GUITHREADINFO
    {
        public int    cbSize;
        public int    flags;
        public IntPtr hwndActive;
        public IntPtr hwndFocus;
        public IntPtr hwndCapture;
        public IntPtr hwndMenuOwner;
        public IntPtr hwndMoveSize;
        public IntPtr hwndCaret;
        public RECT   rectCaret;
    }
}
