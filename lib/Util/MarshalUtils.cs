using System;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Util;

internal static class MarshalUtils
{
    /// <summary>
    ///     Enumerates a buffer containing <see cref="T"/> elements and extracts the native <see cref="T"/> instances.
    /// </summary>
    /// <param name="unmanagedArray">Start of the array buffer.</param>
    /// <param name="elementCount">The amount of elements of <see cref="T"/> in <see cref="unmanagedArray"/>.</param>
    /// <param name="managedArray">An array of extracted managed structure instances.</param>
    /// <typeparam name="T">The type of the extracted structure.</typeparam>
    public static void MarshalUnmanagedArrayToStruct<T>(IntPtr unmanagedArray, int elementCount, out T[] managedArray)
    {
        int size = Marshal.SizeOf(typeof(T));
        managedArray = new T[elementCount];

        for (int i = 0; i < elementCount; i++)
        {
            IntPtr ins = new(unmanagedArray.ToInt64() + (i * size));
            managedArray[i] = Marshal.PtrToStructure<T>(ins);
        }
    }

    /// <summary>
    ///     Enumerates a buffer containing <see cref="T"/> elements and extracts the native <see cref="T"/> instances.
    /// </summary>
    /// <param name="unmanagedArray">Start of the array buffer.</param>
    /// <param name="elementCount">The amount of elements of <see cref="T"/> in <see cref="unmanagedArray"/>.</param>
    /// <param name="managedArray">An array of extracted managed structure instances.</param>
    /// <typeparam name="T">The type of the extracted structure.</typeparam>
    public static unsafe void MarshalUnmanagedArrayToStruct<T>(byte* unmanagedArray, int elementCount, out T[] managedArray)
    {
        int size = Marshal.SizeOf(typeof(T));
        managedArray = new T[elementCount];

        for (int i = 0; i < elementCount; i++)
        {
            IntPtr ins = new(unmanagedArray + (i * size));
            managedArray[i] = Marshal.PtrToStructure<T>(ins);
        }
    }
}