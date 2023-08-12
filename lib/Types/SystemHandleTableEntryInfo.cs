using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Explicit)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct SYSTEM_HANDLE_TABLE_ENTRY_INFO_X64
{
    [FieldOffset(0x00)]
    public UInt32 ProcessId;
    
    [FieldOffset(0x04)]
    public byte ObjectTypeIndex;
    
    [FieldOffset(0x05)]
    public byte HandleAttributes;
    
    [FieldOffset(0x06)]
    public UInt16 HandleValue;
    
    [FieldOffset(0x08)]
    public IntPtr Object;
    
    [FieldOffset(0x10)]
    public UInt32 GrantedAccess;
}