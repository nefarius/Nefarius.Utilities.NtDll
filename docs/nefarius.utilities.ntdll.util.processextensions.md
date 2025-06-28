# ProcessExtensions

Namespace: Nefarius.Utilities.NtDll.Util

Extends  type.

```csharp
public static class ProcessExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ProcessExtensions](./nefarius.utilities.ntdll.util.processextensions.md)

## Methods

### <a id="methods-getsystemhandles"/>**GetSystemHandles(Process)**

Gets all [SystemHandle](./nefarius.utilities.ntdll.handles.systemhandle.md)s associated to `process`.

```csharp
public static IReadOnlyCollection<SystemHandle> GetSystemHandles(Process process)
```

#### Parameters

`process` Process<br>
The  to enumerate.

#### Returns

The list of handles.
