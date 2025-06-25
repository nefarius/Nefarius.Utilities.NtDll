using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Threading;

using Microsoft.Win32.SafeHandles;

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
        ErrorCode = PInvoke.RtlNtStatusToDosError(status);
    }

    internal SystemHandleException(string message, WIN32_ERROR error) : base(message)
    {
        ErrorCode = (uint)error;
    }

    /// <summary>
    ///     The Win32 error code of the failed call.
    /// </summary>
    public uint ErrorCode { get; }

    /// <inheritdoc />
    public override string Message
    {
        get
        {
            Win32Exception win32Exception = new((int)ErrorCode);

            return $"{base.Message} - {win32Exception.Message}";
        }
    }
}

/// <summary>
///     Represents a handle.
/// </summary>
public sealed class SystemHandle
{
    private readonly SYSTEM_HANDLE_TABLE_ENTRY_INFO _handle;

    private SystemHandle(SYSTEM_HANDLE_TABLE_ENTRY_INFO handle)
    {
        _handle = handle;
    }

    /// <summary>
    ///     The unique process ID.
    /// </summary>
    public uint ProcessId => _handle.UniqueProcessId;

    /// <summary>
    ///     The process name.
    /// </summary>
    /// <exception cref="InvalidOperationException">The process the handle belongs to is a system process (PID 4).</exception>
    /// <exception cref="SystemHandleException">Process access or handle duplication failed.</exception>
    public string Name
    {
        get
        {
            if (_handle.UniqueProcessId == 4)
            {
                throw new InvalidOperationException("System processes (PID 4) are not safe and not supported");
            }

            HANDLE process = PInvoke.OpenProcess(
                PROCESS_ACCESS_RIGHTS.PROCESS_DUP_HANDLE,
                false,
                _handle.UniqueProcessId
            );

            if (process.IsNull)
            {
                throw new SystemHandleException("OpenProcess failed", (WIN32_ERROR)Marshal.GetLastWin32Error());
            }

            NTSTATUS status = Native.NtDuplicateObject(
                process,
                (IntPtr)_handle.HandleValue,
                Process.GetCurrentProcess().SafeHandle,
                out SafeFileHandle dupHandle,
                0,
                0,
                0
            );

            if (status != NTSTATUS.STATUS_SUCCESS)
            {
                throw new SystemHandleException("NtDuplicateObject failed", status);
            }

            NtObject obj = NtObject.GetFromHandle(dupHandle);

            dupHandle.Dispose();

            return obj.Name;
        }
    }

    /// <summary>
    ///     Enumerates all open handles on the system.
    /// </summary>
    /// <exception cref="SystemHandleException"></exception>
    public static IEnumerable<SystemHandle> AllHandles
    {
        get
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

                int handleCount = IntPtr.Size == 4 ? Marshal.ReadInt32(handleInfo) : (int)Marshal.ReadInt64(handleInfo);
                IntPtr handleInfoPtr = handleInfo + IntPtr.Size;

                MarshalUtils.MarshalUnmanagedArrayToStruct(
                    handleInfoPtr,
                    handleCount,
                    out SYSTEM_HANDLE_TABLE_ENTRY_INFO[] handleItems
                );

                return handleItems.Select(e => new SystemHandle(e));
            }
            finally
            {
                Marshal.FreeHGlobal(handleInfo);
            }
        }
    }
}