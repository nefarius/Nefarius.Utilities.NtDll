# ProcessExtensions

Namespace: Nefarius.Utilities.NtDll.Util

Extends [Process](https://learn.microsoft.com/dotnet/api/system.diagnostics.process) type.

```csharp
public static class ProcessExtensions
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [ProcessExtensions](./nefarius.utilities.ntdll.util.processextensions.md)<br>
Attributes [ExtensionAttribute](https://learn.microsoft.com/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### <a id="methods-getsystemhandles"/>**GetSystemHandles(Process)**

Gets all [SystemHandle](./nefarius.utilities.ntdll.handles.systemhandle.md)s associated to `process`.

```csharp
public static IReadOnlyCollection<SystemHandle> GetSystemHandles(Process process)
```

#### Parameters

`process` [Process](https://learn.microsoft.com/dotnet/api/system.diagnostics.process)<br>
The [Process](https://learn.microsoft.com/dotnet/api/system.diagnostics.process) to enumerate.

#### Returns

The list of handles.
