using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct SYSTEM_HANDLE_TABLE_ENTRY_INFO
{
    // Information Class 16
    public ushort UniqueProcessId;
    public ushort CreatorBackTraceIndex;
    public byte ObjectTypeIndex;
    public byte HandleAttributes; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
    public ushort HandleValue;
    public IntPtr Object;
    public UInt32 GrantedAccess;
}