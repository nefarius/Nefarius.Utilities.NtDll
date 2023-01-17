using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;
using Windows.Win32.System.WindowsProgramming;

using Microsoft.Win32.SafeHandles;

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

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Local")]
public sealed class NtDirectoryObject
{
    /// <summary>
    ///     Gets the object name without the namespace prefix.
    /// </summary>
    public string Name { get; internal init; }

    /// <summary>
    ///     Gets the object type.
    /// </summary>
    public string TypeName { get; internal init; }

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
    public static unsafe IEnumerable<NtDirectoryObject> GlobalObjects
    {
        get
        {
            SafeFileHandle handle = null;
            OBJECT_ATTRIBUTES attributes = new();

            try
            {
                attributes.Init(GlobalPrefix);

                NTSTATUS status = Native.NtOpenDirectoryObject(out handle,
                    Native.DIRECTORY_QUERY | Native.DIRECTORY_TRAVERSE,
                    ref attributes);

                if (status != NTSTATUS.STATUS_SUCCESS)
                {
                    throw new NtDirectoryObjectException("NtOpenDirectoryObject failed.", status);
                }

                uint ctx = 0, start = 0;
                const int buflen = 1024;
                byte* buffer = stackalloc byte[buflen];
                bool restart = true;
                List<NtDirectoryObject> objects = new();

                while (true)
                {
                    status = Native.NtQueryDirectoryObject(handle, buffer, buflen, false, restart, ref ctx, out _);

                    if (status >= NTSTATUS.STATUS_SUCCESS)
                    {
                        MarshalUnmanagedArrayToStruct(buffer, (int)(ctx - start),
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
                handle?.Dispose();
                attributes.Dispose();
            }
        }
    }

    public override string ToString()
    {
        return $"{Name} ({TypeName})";
    }

    private static void MarshalUnmanagedArrayToStruct<T>(IntPtr unmanagedArray, int length, out T[] managedArray)
    {
        int size = Marshal.SizeOf(typeof(T));
        managedArray = new T[length];

        for (int i = 0; i < length; i++)
        {
            IntPtr ins = new(unmanagedArray.ToInt64() + (i * size));
            managedArray[i] = Marshal.PtrToStructure<T>(ins);
        }
    }

    private static unsafe void MarshalUnmanagedArrayToStruct<T>(byte* unmanagedArray, int length, out T[] managedArray)
    {
        int size = Marshal.SizeOf(typeof(T));
        managedArray = new T[length];

        for (int i = 0; i < length; i++)
        {
            IntPtr ins = new(unmanagedArray + (i * size));
            managedArray[i] = Marshal.PtrToStructure<T>(ins);
        }
    }
}