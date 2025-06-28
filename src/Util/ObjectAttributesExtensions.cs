using System;
using System.Runtime.InteropServices;

using Windows.Wdk.Foundation;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Nefarius.Utilities.NtDll.Util;

internal static class ObjectAttributesExtensions
{
    internal static unsafe void Init(this ref OBJECT_ATTRIBUTES attributes, string objectName,
        bool caseInsensitive = true)
    {
        UNICODE_STRING name = new()
        {
            Length = (ushort)(objectName.Length * 2),
            MaximumLength = (ushort)(objectName.Length * 2),
            Buffer = new PWSTR((char*)Marshal.StringToHGlobalUni(objectName).ToPointer())
        };

        IntPtr nameBuffer = Marshal.AllocHGlobal(Marshal.SizeOf<UNICODE_STRING>());
        Marshal.StructureToPtr(name, nameBuffer, false);

        attributes.Attributes = caseInsensitive ? PInvoke.OBJ_CASE_INSENSITIVE : 0U;
        attributes.Length = (uint)Marshal.SizeOf<OBJECT_ATTRIBUTES>();
        attributes.ObjectName = (UNICODE_STRING*)nameBuffer.ToPointer();
    }

    internal static unsafe void Dispose(this ref OBJECT_ATTRIBUTES attributes)
    {
        Marshal.FreeHGlobal(new IntPtr(attributes.ObjectName->Buffer));
        Marshal.FreeHGlobal(new IntPtr(attributes.ObjectName));
    }
}