using System;
using System.Runtime.InteropServices;

using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.WindowsProgramming;

using JetBrains.Annotations;

using Microsoft.Win32.SafeHandles;

namespace Nefarius.Drivers.NtDll.Objects;

/// <summary>
///     Potential exception <see cref="NtObject"/> can throw.
/// </summary>
public sealed class NtObjectException : Exception
{
    internal NtObjectException(string message, NTSTATUS status) : base(message)
    {
        Status = status;
    }

    /// <summary>
    ///     The NTSTATUS code of the failed call.
    /// </summary>
    [UsedImplicitly]
    public uint Status { get; }
}

public sealed class NtObject
{
    [UsedImplicitly]
    public string Name { get; internal set; }

    [UsedImplicitly]
    public static unsafe NtObject GetFromHandle(SafeFileHandle handle)
    {
        UNICODE_STRING objName;
        uint sizeRequired = 0;
        NTSTATUS ret = PInvoke.NtQueryObject(handle, (OBJECT_INFORMATION_CLASS)1, &objName,
            (uint)Marshal.SizeOf<UNICODE_STRING>(), &sizeRequired);

        if (ret != NTSTATUS.STATUS_SUCCESS)
        {
            throw new NtObjectException("NtQueryObject failed.", ret);
        }

        char* buffer = stackalloc char[(int)sizeRequired];

        ret = PInvoke.NtQueryObject(handle, (OBJECT_INFORMATION_CLASS)1, buffer, sizeRequired,
            &sizeRequired);

        if (ret != NTSTATUS.STATUS_SUCCESS)
        {
            throw new NtObjectException("NtQueryObject failed.", ret);
        }

        UNICODE_STRING* objNameFull = (UNICODE_STRING*)buffer;

        return new NtObject { Name = new string(objNameFull->Buffer, 0, objNameFull->Length / 2) };
    }
}