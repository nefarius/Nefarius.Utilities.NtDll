# SystemHandle

Namespace: Nefarius.Utilities.NtDll.Handles

Represents a handle.

```csharp
public sealed class SystemHandle
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SystemHandle](./nefarius.utilities.ntdll.handles.systemhandle.md)

## Properties

### <a id="properties-allhandles"/>**AllHandles**

Lists all open handles on the system.

```csharp
public static IReadOnlyCollection<SystemHandle> AllHandles { get; }
```

#### Property Value

[IReadOnlyCollection&lt;SystemHandle&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

#### Exceptions

[SystemHandleException](./nefarius.utilities.ntdll.handles.systemhandleexception.md)<br>

### <a id="properties-name"/>**Name**

The process name.

```csharp
public string Name { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
The process the handle belongs to is a system process (PID 4).

[SystemHandleException](./nefarius.utilities.ntdll.handles.systemhandleexception.md)<br>
Process access or handle duplication failed.

### <a id="properties-processid"/>**ProcessId**

The unique process ID.

```csharp
public uint ProcessId { get; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
