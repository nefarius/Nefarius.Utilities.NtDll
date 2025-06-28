using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Wdk.Foundation;
using Windows.Win32;
using Windows.Win32.Foundation;

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
        ErrorCode = PInvoke.RtlNtStatusToDosError(status);
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
///     A symbolic link object.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtSymbolicLinkObject
{
    /// <summary>
    ///     Gets the target of the symbolic link.
    /// </summary>
    public string LinkTarget { get; internal set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return LinkTarget;
    }

    /// <summary>
    ///     Resolves the target for a symbolic link object.
    /// </summary>
    /// <param name="obj">The <see cref="NtDirectoryObject" /> to resolve.</param>
    /// <returns>The resolved <see cref="NtSymbolicLinkObject" />.</returns>
    public static NtSymbolicLinkObject GetLinkTarget(NtDirectoryObject obj)
    {
        return GetLinkTarget(obj.FullName);
    }

    /// <summary>
    ///     Resolves the target for a symbolic link object.
    /// </summary>
    /// <param name="objectName">The name of the object.</param>
    /// <returns>The resolved <see cref="NtSymbolicLinkObject" />.</returns>
    /// <exception cref="NtSymbolicLinkObjectException">Probably not a symbolic link object or access issue.</exception>
    public static unsafe NtSymbolicLinkObject GetLinkTarget(string objectName)
    {
        OBJECT_ATTRIBUTES attributes = new();

        attributes.Init(objectName);

        NTSTATUS ret = Windows.Wdk.PInvoke.NtOpenSymbolicLinkObject(
            out HANDLE handle,
            0x80000000U,
            in attributes
        );

        if (ret == NTSTATUS.STATUS_ACCESS_DENIED)
        {
            return null;
        }

        if (ret != NTSTATUS.STATUS_SUCCESS)
        {
            throw new NtSymbolicLinkObjectException("NtOpenSymbolicLinkObject failed.", ret);
        }

        UNICODE_STRING target = new();
        uint len = 0;

        ret = Windows.Wdk.PInvoke.NtQuerySymbolicLinkObject(handle, &target, &len);

        if (ret != NTSTATUS.STATUS_BUFFER_TOO_SMALL)
        {
            throw new NtSymbolicLinkObjectException("NtQuerySymbolicLinkObject failed.", ret);
        }

        target.Buffer = new PWSTR((char*)Marshal.AllocHGlobal((int)len * 2));
        target.Length = (ushort)(len * 2);
        target.MaximumLength = (ushort)(len * 2);

        try
        {
            ret = Windows.Wdk.PInvoke.NtQuerySymbolicLinkObject(handle, &target, &len);

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