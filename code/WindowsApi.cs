using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace LightFileExplorer
{
    internal static class WindowsApi
    {
        internal const int IDI_APPLICATION = 32512;

        internal const uint LVM_SETTEXTBKCOLOR = 0x1026;

        [ComImport, Guid("0000000A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface ILockBytes
        {
            void ReadAt([In, MarshalAs(UnmanagedType.U8)] long ulOffset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] pcbRead);

            void WriteAt([In, MarshalAs(UnmanagedType.U8)] long ulOffset, IntPtr pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] pcbWritten);

            void Flush();

            void SetSize([In, MarshalAs(UnmanagedType.U8)] long cb);

            void LockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int dwLockType);

            void UnlockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int dwLockType);

            void Stat([Out] out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In, MarshalAs(UnmanagedType.U4)] int grfStatFlag);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000B-0000-0000-C000-000000000046")]
        internal interface IStorage
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            IStream CreateStream([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStream OpenStream([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr reserved1, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage CreateStorage([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.U4)] int grfMode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage OpenStorage([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr pstgPriority, [In, MarshalAs(UnmanagedType.U4)] int grfMode, IntPtr snbExclude, [In, MarshalAs(UnmanagedType.U4)] int reserved);

            void CopyTo(int ciidExclude, [In, MarshalAs(UnmanagedType.LPArray)] Guid[] pIIDExclude, IntPtr snbExclude, [In, MarshalAs(UnmanagedType.Interface)] IStorage stgDest);

            void MoveElementTo([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In, MarshalAs(UnmanagedType.Interface)] IStorage stgDest, [In, MarshalAs(UnmanagedType.BStr)] string pwcsNewName, [In, MarshalAs(UnmanagedType.U4)] int grfFlags);

            void Commit(int grfCommitFlags);

            void Revert();

            void EnumElements([In, MarshalAs(UnmanagedType.U4)] int reserved1, IntPtr reserved2, [In, MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

            void DestroyElement([In, MarshalAs(UnmanagedType.BStr)] string pwcsName);

            void RenameElement([In, MarshalAs(UnmanagedType.BStr)] string pwcsOldName, [In, MarshalAs(UnmanagedType.BStr)] string pwcsNewName);

            void SetElementTimes([In, MarshalAs(UnmanagedType.BStr)] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

            void SetClass([In] ref Guid clsid);

            void SetStateBits(int grfStateBits, int grfMask);

            void Stat([Out] out System.Runtime.InteropServices.ComTypes.STATSTG pStatStg, int grfStatFlag);
        }

        [DllImport("Ole32", PreserveSig = false)]
        internal static extern ILockBytes CreateILockBytesOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease);

        [DllImport("Kernel32", SetLastError = true)]
        internal static extern bool FindClose(IntPtr hFindFile);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindFirstFile(string fileName, out WIN32_FIND_DATA findFileData);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool FindNextFile(IntPtr findFileHandle, out WIN32_FIND_DATA findFileData);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetFileAttributes(string fileName);

        [DllImport("Ole32", CharSet = CharSet.Auto, PreserveSig = false)]
        internal static extern IntPtr GetHGlobalFromILockBytes(ILockBytes pLockBytes);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("Kernel32")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadIcon(IntPtr handle, IntPtr iconName);

        [DllImport("ShlwApi", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool PathFileExists(string path);

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetFileAttributes(string fileName, FileAttributes fileAttributes);

        [DllImport("Ole32", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes plkbyt, uint grfMode, uint reserved);

        [BestFitMapping(false)]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct WIN32_FIND_DATA
        {
            internal FileAttributes dwFileAttributes;

            internal uint ftCreationTimeLo;

            internal uint ftCreationTimeHi;

            internal uint ftLastAccessTimeLo;

            internal uint ftLastAccessTimeHi;

            internal uint ftLastWriteTimeLo;

            internal uint ftLastWriteTimeHi;

            internal uint nFileSizeHi;

            internal uint nFileSizeLo;

            internal uint dwReserved0;

            internal uint dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            internal string cAlternate;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal sealed class FILEDESCRIPTORA
        {
            internal uint dwFlags;

            internal Guid clsid;

            internal SIZEL sizel;

            internal POINTL pointl;

            internal uint dwFileAttributes;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

            internal uint nFileSizeHigh;

            internal uint nFileSizeLow;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal sealed class FILEDESCRIPTORW
        {
            internal uint dwFlags;

            internal Guid clsid;

            internal SIZEL sizel;

            internal POINTL pointl;

            internal uint dwFileAttributes;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;

            internal System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;

            internal uint nFileSizeHigh;

            internal uint nFileSizeLow;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal sealed class FILEGROUPDESCRIPTORA
        {
            internal uint cItems;

            internal FILEDESCRIPTORA[] fgd;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal sealed class FILEGROUPDESCRIPTORW
        {
            internal uint cItems;

            internal FILEDESCRIPTORW[] fgd;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal sealed class POINTL
        {
            internal int x;

            internal int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal sealed class SIZEL
        {
            internal int cx;

            internal int cy;
        }
    }
}