# SystemHandle

Namespace: Nefarius.Utilities.NtDll.Handles

Represents a handle.

```csharp
public sealed class SystemHandle
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SystemHandle](./nefarius.utilities.ntdll.handles.systemhandle.md)

## Properties

### <a id="properties-allhandles"/>**AllHandles**

Enumerates all open handles on the system.

```csharp
public static IEnumerable<SystemHandle> AllHandles { get; }
```

#### Property Value

[IEnumerable&lt;SystemHandle&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Exceptions

[SystemHandleException](./nefarius.utilities.ntdll.handles.systemhandleexception.md)<br>

### <a id="properties-name"/>**Name**

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-processid"/>**ProcessId**

```csharp
public uint ProcessId { get; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
