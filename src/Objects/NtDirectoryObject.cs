using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Windows.Wdk;
using Windows.Wdk.Foundation;
using Windows.Win32.Foundation;

using Nefarius.Utilities.NtDll.Types;
using Nefarius.Utilities.NtDll.Util;

namespace Nefarius.Utilities.NtDll.Objects;

/// <summary>
///     Potential exception <see cref="NtDirectoryObject" /> can throw.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public sealed class NtDirectoryObjectException : Exception
{
    internal NtDirectoryObjectException(string message, NTSTATUS status) : base(message)
    {
        Status = status;
    }

    /// <summary>
    ///     The NTSTATUS code of the failed call.
    /// </summary>
    public uint Status { get; }
}

/// <summary>
///     A generic NT object.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
public sealed class NtDirectoryObject
{
    /// <summary>
    ///     Gets the object name without the namespace prefix.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    ///     Gets the object type.
    /// </summary>
    public string TypeName { get; internal set; }

    /// <summary>
    ///     Gets the full object path including the namespace.
    /// </summary>
    public string FullName => $"{GlobalPrefix}\\{Name}";

    /// <summary>
    ///     Gets the symbolic link path in a format for CreateFile, CM APIs etc.
    /// </summary>
    public string Path => @$"\\?\{Name}";

    /// <summary>
    ///     Gets the global objects namespace prefix.
    /// </summary>
    public static string GlobalPrefix => @"\GLOBAL??";

    /// <summary>
    ///     True if this object is a symbolic link, false otherwise.
    /// </summary>
    public bool IsSymbolicLink => TypeName == "SymbolicLink";

    /// <summary>
    ///     Gets all global objects.
    /// </summary>
    public static unsafe IReadOnlyCollection<NtDirectoryObject> GlobalObjects
    {
        get
        {
            HANDLE handle = HANDLE.Null;
            OBJECT_ATTRIBUTES attributes = new();

            try
            {
                attributes.Init(GlobalPrefix);

                NTSTATUS status = PInvoke.NtOpenDirectoryObject(
                    out handle,
                    PInvoke.DIRECTORY_QUERY | PInvoke.DIRECTORY_TRAVERSE,
                    in attributes
                );

                if (status != NTSTATUS.STATUS_SUCCESS)
                {
                    throw new NtDirectoryObjectException("NtOpenDirectoryObject failed.", status);
                }

                uint ctx = 0, start = 0;
                const int buflen = 1024;
                byte* buffer = stackalloc byte[buflen];
                bool restart = true;
                List<NtDirectoryObject> objects = [];

                while (true)
                {
                    uint returnLength = 0;
                    status = PInvoke.NtQueryDirectoryObject(
                        handle, buffer,
                        buflen,
                        new BOOLEAN(false),
                        new BOOLEAN(restart),
                        ref ctx,
                        &returnLength
                    );

                    if (status >= NTSTATUS.STATUS_SUCCESS)
                    {
                        MarshalUtils.MarshalUnmanagedArrayToStruct(buffer, (int)(ctx - start),
                            out OBJECT_DIRECTORY_INFORMATION[] items);

                        objects.AddRange(items.Select(info => new NtDirectoryObject
                        {
                            Name = new string(info.Name.Buffer.Value, 0, info.Name.Length / 2),
                            TypeName = new string(info.TypeName.Buffer.Value, 0, info.TypeName.Length / 2)
                        }));
                    }

                    if (status == NTSTATUS.STATUS_MORE_ENTRIES)
                    {
                        start = ctx;
                        restart = false;
                    }

                    if (status == NTSTATUS.STATUS_SUCCESS || status == NTSTATUS.STATUS_NO_MORE_ENTRIES)
                    {
                        break;
                    }
                }

                return objects;
            }
            finally
            {
                if (handle != HANDLE.Null)
                {
                    Windows.Win32.PInvoke.CloseHandle(handle);
                }

                attributes.Dispose();
            }
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Name} ({TypeName})";
    }
}