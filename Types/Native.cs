using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;
using Windows.Win32.System.WindowsProgramming;

using Microsoft.Win32.SafeHandles;

namespace Nefarius.Utilities.NtDll.Types;

/*
 * These types are not yet available through CsWin32
 */

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct OBJECT_DIRECTORY_INFORMATION
{
    public UNICODE_STRING Name;
    public UNICODE_STRING TypeName;
}

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
internal static class Native
{
    internal const uint DIRECTORY_QUERY = 0x0001;
    internal const uint DIRECTORY_TRAVERSE = 0x0002;
    internal const uint DIRECTORY_CREATE_OBJECT = 0x0004;
    internal const uint DIRECTORY_CREATE_SUBDIRECTORY = 0x0008;

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtOpenSymbolicLinkObject(
        out SafeFileHandle linkHandle,
        uint desiredAccess,
        ref OBJECT_ATTRIBUTES objectAttributes
    );

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtQuerySymbolicLinkObject(
        SafeFileHandle linkHandle,
        ref UNICODE_STRING linkTarget,
        out int returnedLength
    );

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtOpenDirectoryObject(
        out SafeFileHandle directoryHandle,
        uint desiredAccess,
        ref OBJECT_ATTRIBUTES objectAttributes
    );

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtQueryDirectoryObject(
        SafeFileHandle directoryHandle,
        IntPtr buffer,
        int length,
        bool returnSingleEntry,
        bool restartScan,
        ref uint context,
        out uint returnLength
    );

    [DllImport("ntdll.dll")]
    internal static unsafe extern NTSTATUS NtQueryDirectoryObject(
        SafeFileHandle directoryHandle,
        void* buffer,
        uint length,
        bool returnSingleEntry,
        bool restartScan,
        ref uint context,
        out uint returnLength
    );

    [DllImport("ntdll.dll", PreserveSig = false)]
    internal static extern void NtQuerySystemInformation(
        SYSTEM_INFORMATION_CLASS SystemInformationClass,
        IntPtr SystemInformation,
        uint SystemInformationLength,
        out uint ReturnLength
    );

    [DllImport("ntdll.dll", PreserveSig = false)]
    internal static unsafe extern void NtQuerySystemInformation(
        SYSTEM_INFORMATION_CLASS SystemInformationClass,
        void* SystemInformation,
        uint SystemInformationLength,
        out uint ReturnLength
    );

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtDuplicateObject(
        SafeHandle SourceProcessHandle,
        IntPtr SourceHandle,
        SafeHandle TargetProcessHandle,
        out IntPtr TargetHandle,
        UInt32 DesiredAccess,
        UInt32 HandleAttributes,
        UInt32 Options
    );

    [DllImport("ntdll.dll")]
    internal static extern NTSTATUS NtDuplicateObject(
        SafeHandle SourceProcessHandle,
        IntPtr SourceHandle,
        IntPtr TargetProcessHandle,
        IntPtr TargetHandle,
        UInt32 DesiredAccess,
        UInt32 HandleAttributes,
        UInt32 Options
    );

    [DllImport("ntdll.dll")]
    internal static extern UInt32 NtQueryObject(
        IntPtr objectHandle,
        OBJECT_INFORMATION_CLASS informationClass,
        IntPtr informationPtr,
        UInt32 informationLength,
        ref UInt32 returnLength
    );
    
    [DllImport("ntdll.dll")]
    internal static unsafe extern NTSTATUS NtQueryObject(
        HANDLE objectHandle,
        OBJECT_INFORMATION_CLASS informationClass,
        [Optional] void* informationPtr,
        UInt32 informationLength,
        [Optional] uint* returnLength
    );
}