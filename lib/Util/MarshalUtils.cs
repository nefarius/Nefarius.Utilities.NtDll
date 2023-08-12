using System;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Util;

internal class MarshalUtils
{
    public static void MarshalUnmanagedArrayToStruct<T>(IntPtr unmanagedArray, int length, out T[] managedArray)
    {
        int size = Marshal.SizeOf(typeof(T));
        managedArray = new T[length];

        for (int i = 0; i < length; i++)
        {
            IntPtr ins = new(unmanagedArray.ToInt64() + (i * size));
            managedArray[i] = Marshal.PtrToStructure<T>(ins);
        }
    }

    public static unsafe void MarshalUnmanagedArrayToStruct<T>(byte* unmanagedArray, int length, out T[] managedArray)
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