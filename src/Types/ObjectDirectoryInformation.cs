using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Windows.Win32.Foundation;

namespace Nefarius.Utilities.NtDll.Types;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "InconsistentNaming")]
internal struct OBJECT_DIRECTORY_INFORMATION
{
    public UNICODE_STRING Name;
    public UNICODE_STRING TypeName;
}