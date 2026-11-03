# <img src="assets/NSS-128x128.png" align="left" />Nefarius.Utilities.NtDll

[![.NET](https://github.com/nefarius/Nefarius.Utilities.NtDll/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/Nefarius.Utilities.NtDll/actions/workflows/build.yml)
![Requirements](https://img.shields.io/badge/Requires-.NET%20Standard%202.0%20%7C%20.NET%20Framework%204.6.2%20%7C%20.NET%208%20%7C%20.NET%2010-blue.svg)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.Utilities.NtDll)](https://www.nuget.org/packages/Nefarius.Utilities.NtDll/)
[![Nuget](https://img.shields.io/nuget/dt/Nefarius.Utilities.NtDll)](https://www.nuget.org/packages/Nefarius.Utilities.NtDll/)

Managed wrappers around NTDLL native APIs on Windows.

## Features

- Enumerate open handles on the system
- Resolve the NT object name of a handle
- List objects in the `\GLOBAL??` object directory
- Resolve NT symbolic links (for example `\GLOBAL??\C:` to `\Device\...`)
- `Process.GetSystemHandles()` extension for the handles owned by one process

## Testing

See [TESTING.md](TESTING.md) for Unit/CI filters, admin prerequisites, and how tests run in GitHub Actions.

## Documentation

[API documentation](https://github.com/nefarius/Nefarius.Utilities.NtDll/blob/master/docs/index.md).

## Sources & 3rd party credits

- [XMLDoc2Markdown](https://charlesdevandiere.github.io/xmldoc2md/)
- [HOWTO: Enumerate handles](http://web.archive.org/web/20161210010928/http://forum.sysinternals.com/howto-enumerate-handles_topic18892.html)
  - [Sample code](http://web.archive.org/web/20161210010928/http://forum.sysinternals.com/uploads/26792/handles.zip)
- [LockLess](https://github.com/GhostPack/Lockless)
- [SYSTEM_HANDLE_TABLE_ENTRY_INFO](https://www.geoffchappell.com/studies/windows/km/ntoskrnl/api/ex/sysinfo/handle_table_entry.htm)
- [OBJECT_INFORMATION_CLASS](https://geoffchappell.com/studies/windows/km/ntoskrnl/inc/api/ntobapi_x/object_information_class.htm)
