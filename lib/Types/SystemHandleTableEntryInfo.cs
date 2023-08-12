using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct SYSTEM_HANDLE_TABLE_ENTRY_INFO
{
    public UInt32 ProcessId;
    public byte ObjectTypeNumber;
    public byte Flags;
    public UInt16 Handle;
    public IntPtr Object;
    public UInt32 GrantedAccess;
}