# NtObject

Namespace: Nefarius.Utilities.NtDll.Objects

Represents an NT Object.

```csharp
public sealed class NtObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NtObject](./nefarius.utilities.ntdll.objects.ntobject.md)

## Properties

### <a id="properties-name"/>**Name**

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

### <a id="methods-getfromhandle"/>**GetFromHandle(SafeFileHandle)**

```csharp
public static NtObject GetFromHandle(SafeFileHandle handle)
```

#### Parameters

`handle` [SafeFileHandle](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.safehandles.safefilehandle)<br>

#### Returns

[NtObject](./nefarius.utilities.ntdll.objects.ntobject.md)
