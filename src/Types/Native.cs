using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Wdk.Foundation;
using Windows.Win32.Foundation;

using Microsoft.Win32.SafeHandles;

namespace Nefarius.Utilities.NtDll.Types;

/*
 * These types are not yet available through CsWin32
 */

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct OBJECT_DIRECTORY_INFORMATION
{
    public UNICODE_STRING Name;
    public UNICODE_STRING TypeName;
}

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
internal static class Native
{
    internal const uint DIRECTORY_QUERY = 0x0001;
    internal const uint DIRECTORY_TRAVERSE = 0x0002;
    internal const uint DIRECTORY_CREATE_OBJECT = 0x0004;
    internal const uint DIRECTORY_CREATE_SUBDIRECTORY = 0x0008;

}