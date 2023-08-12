using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;

using Nefarius.Utilities.NtDll.Objects;
using Nefarius.Utilities.NtDll.Types;
using Nefarius.Utilities.NtDll.Util;

namespace Nefarius.Utilities.NtDll.Handles;

/// <summary>
///     Potential exception <see cref="NtObject" /> can throw.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class SystemHandleException : Exception
{
    internal SystemHandleException(string message, NTSTATUS status) : base(message)
    {
        Status = status;
    }

    /// <summary>
    ///     The NTSTATUS code of the failed call.
    /// </summary>
    public uint Status { get; }
}

public sealed class SystemHandle
{
    public static void Test()
    {
        uint handleInfoSize = 0x10000;
        IntPtr handleInfo = Marshal.AllocHGlobal((int)handleInfoSize);

        try
        {
            NTSTATUS status;

            while ((status = Native.NtQuerySystemInformation(
                       SYSTEM_INFORMATION_CLASS.SystemHandleInformation,
                       handleInfo,
                       handleInfoSize,
                       out _
                   )) == NTSTATUS.STATUS_INFO_LENGTH_MISMATCH)
            {
                handleInfoSize *= 2;
                Marshal.FreeHGlobal(handleInfo);
                handleInfo = Marshal.AllocHGlobal((int)handleInfoSize);
            }

            if (status != NTSTATUS.STATUS_SUCCESS)
            {
                throw new SystemHandleException("NtQuerySystemInformation failed", status);
            }

            int handleCount = Marshal.ReadInt32(handleInfo);
            IntPtr handleInfoPtr = handleInfo + sizeof(int);

            MarshalUtils.MarshalUnmanagedArrayToStruct(
                handleInfoPtr,
                handleCount,
                out SYSTEM_HANDLE_TABLE_ENTRY_INFO[] handleItems
            );

            foreach (SYSTEM_HANDLE_TABLE_ENTRY_INFO handle in handleItems)
            {
                if (handle.ProcessId != 0)
                {
                    Debugger.Break();
                }
            }
        }
        finally
        {
            Marshal.FreeHGlobal(handleInfo);
        }
    }
}