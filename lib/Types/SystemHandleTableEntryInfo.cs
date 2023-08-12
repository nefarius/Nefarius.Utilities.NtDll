using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct SYSTEM_HANDLE_TABLE_ENTRY_INFO
{
    public UInt32 UniqueProcessId;
    public Byte ObjectTypeIndex;
    public Byte HandleAttributes;
    public UInt16 HandleValue;
    public IntPtr Object;
    public UInt32 GrantedAccess;
}