﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Wdk.Foundation;
using Windows.Win32.Foundation;

using Microsoft.Win32.SafeHandles;

using Nefarius.Utilities.NtDll.Types;

namespace Nefarius.Utilities.NtDll.Objects;

/// <summary>
///     Potential exception <see cref="NtObject" /> can throw.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtObjectException : Exception
{
    internal NtObjectException(string message, NTSTATUS status) : base(message)
    {
        Status = status;
    }

    /// <summary>
    ///     The NTSTATUS code of the failed call.
    /// </summary>
    public uint Status { get; }
}

/// <summary>
///     Represents an NT Object.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtObject
{
    public string Name { get; internal set; }

    public static unsafe NtObject GetFromHandle(SafeFileHandle handle)
    {
        UNICODE_STRING objName;
        uint sizeRequired = 0;
        NTSTATUS ret = Native.NtQueryObject(handle, (OBJECT_INFORMATION_CLASS)1, &objName,
            (uint)Marshal.SizeOf<UNICODE_STRING>(), &sizeRequired);

        if (ret != NTSTATUS.STATUS_SUCCESS /* && ret != NTSTATUS.STATUS_BUFFER_OVERFLOW */)
        {
            throw new NtObjectException("NtQueryObject failed.", ret);
        }

        char* buffer = stackalloc char[(int)sizeRequired];

        ret = Native.NtQueryObject(handle, (OBJECT_INFORMATION_CLASS)1, buffer, sizeRequired,
            &sizeRequired);

        if (ret != NTSTATUS.STATUS_SUCCESS)
        {
            throw new NtObjectException("NtQueryObject failed.", ret);
        }

        UNICODE_STRING* objNameFull = (UNICODE_STRING*)buffer;

        return new NtObject { Name = new string(objNameFull->Buffer, 0, objNameFull->Length / 2) };
    }
}