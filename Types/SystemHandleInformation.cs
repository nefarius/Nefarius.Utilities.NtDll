using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct SYSTEM_HANDLE_INFORMATION
{
    public UInt32 HandleCount;
    public IntPtr Handles;
}