# NtSymbolicLinkObject

Namespace: Nefarius.Utilities.NtDll.Objects

```csharp
public sealed class NtSymbolicLinkObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md)

## Properties

### <a id="properties-linktarget"/>**LinkTarget**

```csharp
public string LinkTarget { get; internal set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### <a id="constructors-.ctor"/>**NtSymbolicLinkObject()**

```csharp
public NtSymbolicLinkObject()
```

## Methods

### <a id="methods-getlinktarget"/>**GetLinkTarget(NtDirectoryObject)**

```csharp
public static NtSymbolicLinkObject GetLinkTarget(NtDirectoryObject obj)
```

#### Parameters

`obj` [NtDirectoryObject](./nefarius.utilities.ntdll.objects.ntdirectoryobject.md)<br>

#### Returns

[NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md)

### <a id="methods-getlinktarget"/>**GetLinkTarget(String)**

```csharp
public static NtSymbolicLinkObject GetLinkTarget(string objectName)
```

#### Parameters

`objectName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md)

### <a id="methods-tostring"/>**ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)
