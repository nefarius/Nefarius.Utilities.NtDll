# NtObject

Namespace: Nefarius.Utilities.NtDll.Objects

Represents an NT Object.

```csharp
public sealed class NtObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NtObject](./nefarius.utilities.ntdll.objects.ntobject.md)

## Properties

### <a id="properties-name"/>**Name**

Gets the name of the object.

```csharp
public string Name { get; internal set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### <a id="constructors-.ctor"/>**NtObject()**

```csharp
public NtObject()
```

## Methods

### <a id="methods-getfromhandle"/>**GetFromHandle(HANDLE)**

```csharp
internal static NtObject GetFromHandle(HANDLE handle)
```

#### Parameters

`handle` [HANDLE](./windows.win32.foundation.handle.md)<br>

#### Returns

[NtObject](./nefarius.utilities.ntdll.objects.ntobject.md)
