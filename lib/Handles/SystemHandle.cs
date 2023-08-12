using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;

using Nefarius.Utilities.NtDll.Objects;
using Nefarius.Utilities.NtDll.Types;

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

            var handleInfoHeader = Marshal.PtrToStructure<SYSTEM_HANDLE_INFORMATION>(handleInfo);
        }
        finally
        {
            Marshal.FreeHGlobal(handleInfo);
        }
    }
}