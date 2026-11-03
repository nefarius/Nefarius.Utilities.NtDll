using System.Runtime.InteropServices;

using Nefarius.Utilities.NtDll.Util;

namespace Tests.Unit;

[TestFixture]
[Category(TestCategories.Unit)]
internal class MarshalUtilsTests
{
    [Test]
    public void MarshalUnmanagedArrayToStruct_RoundTripsStructArray()
    {
        Sample[] source = [new Sample(1, 2), new Sample(3, 4), new Sample(5, 6)];
        int size = Marshal.SizeOf<Sample>();
        IntPtr buffer = Marshal.AllocHGlobal(size * source.Length);

        try
        {
            for (int i = 0; i < source.Length; i++)
            {
                Marshal.StructureToPtr(source[i], buffer + (i * size), false);
            }

            MarshalUtils.MarshalUnmanagedArrayToStruct(buffer, source.Length, out Sample[] managed);

            Assert.That(managed, Is.EqualTo(source));
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    [Test]
    public unsafe void MarshalUnmanagedArrayToStruct_BytePointer_RoundTripsStructArray()
    {
        Sample[] source = [new Sample(7, 8), new Sample(9, 10)];
        int size = Marshal.SizeOf<Sample>();
        byte* buffer = stackalloc byte[size * source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            Marshal.StructureToPtr(source[i], new IntPtr(buffer + (i * size)), false);
        }

        MarshalUtils.MarshalUnmanagedArrayToStruct(buffer, source.Length, out Sample[] managed);

        Assert.That(managed, Is.EqualTo(source));
    }

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct Sample(int a, int b)
    {
        public readonly int A = a;
        public readonly int B = b;
    }
}
