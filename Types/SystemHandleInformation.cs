using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public struct SYSTEM_HANDLE_INFORMATION
{
    public int ProcessID;
    public byte ObjectTypeNumber;
    public byte Flags; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
    public ushort Handle;
    public int Object_Pointer;
    public UInt32 GrantedAccess;
}