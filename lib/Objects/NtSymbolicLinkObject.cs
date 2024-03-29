﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Wdk.Foundation;
using Windows.Win32.Foundation;

using Microsoft.Win32.SafeHandles;

using Nefarius.Utilities.NtDll.Types;
using Nefarius.Utilities.NtDll.Util;

namespace Nefarius.Utilities.NtDll.Objects;

/// <summary>
///     Potential exception <see cref="NtSymbolicLinkObject" /> can throw.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtSymbolicLinkObjectException : Exception
{
    internal NtSymbolicLinkObjectException(string message, NTSTATUS status) : base(message)
    {
        Status = status;
    }

    /// <summary>
    ///     The NTSTATUS code of the failed call.
    /// </summary>
    public uint Status { get; }
}

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtSymbolicLinkObject
{
    public string LinkTarget { get; internal set; }

    public override string ToString()
    {
        return LinkTarget;
    }

    public static NtSymbolicLinkObject GetLinkTarget(NtDirectoryObject obj)
    {
        return GetLinkTarget(obj.FullName);
    }

    public static unsafe NtSymbolicLinkObject GetLinkTarget(string objectName)
    {
        OBJECT_ATTRIBUTES attributes = new();

        attributes.Init(objectName);

        NTSTATUS ret = Native.NtOpenSymbolicLinkObject(out SafeFileHandle handle, 0x80000000U, ref attributes);

        if (ret == NTSTATUS.STATUS_ACCESS_DENIED)
        {
            return null;
        }

        if (ret != NTSTATUS.STATUS_SUCCESS)
        {
            throw new NtSymbolicLinkObjectException("NtOpenSymbolicLinkObject failed.", ret);
        }

        UNICODE_STRING target = new();

        ret = Native.NtQuerySymbolicLinkObject(handle, ref target, out int len);

        if (ret != NTSTATUS.STATUS_BUFFER_TOO_SMALL)
        {
            throw new NtSymbolicLinkObjectException("NtQuerySymbolicLinkObject failed.", ret);
        }

        target.Buffer = new PWSTR((char*)Marshal.AllocHGlobal(len * 2));
        target.Length = (ushort)(len * 2);
        target.MaximumLength = (ushort)(len * 2);

        try
        {
            ret = Native.NtQuerySymbolicLinkObject(handle, ref target, out len);

            if (ret != NTSTATUS.STATUS_SUCCESS)
            {
                throw new NtSymbolicLinkObjectException("NtQuerySymbolicLinkObject failed.", ret);
            }

            NtSymbolicLinkObject slo = new() { LinkTarget = new string(target.Buffer) };

            return string.IsNullOrWhiteSpace(slo.LinkTarget) ? null : slo;
        }
        finally
        {
            Marshal.FreeHGlobal(new IntPtr(target.Buffer));
        }
    }
}