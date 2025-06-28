# NtSymbolicLinkObject

Namespace: Nefarius.Utilities.NtDll.Objects

A symbolic link object.

```csharp
public sealed class NtSymbolicLinkObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md)

## Properties

### <a id="properties-linktarget"/>**LinkTarget**

Gets the target of the symbolic link.

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

Resolves the target for a symbolic link object.

```csharp
public static NtSymbolicLinkObject GetLinkTarget(NtDirectoryObject obj)
```

#### Parameters

`obj` [NtDirectoryObject](./nefarius.utilities.ntdll.objects.ntdirectoryobject.md)<br>
The [NtDirectoryObject](./nefarius.utilities.ntdll.objects.ntdirectoryobject.md) to resolve.

#### Returns

The resolved [NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md).

### <a id="methods-getlinktarget"/>**GetLinkTarget(String)**

Resolves the target for a symbolic link object.

```csharp
public static NtSymbolicLinkObject GetLinkTarget(string objectName)
```

#### Parameters

`objectName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the object.

#### Returns

The resolved [NtSymbolicLinkObject](./nefarius.utilities.ntdll.objects.ntsymboliclinkobject.md).

#### Exceptions

[NtSymbolicLinkObjectException](./nefarius.utilities.ntdll.objects.ntsymboliclinkobjectexception.md)<br>
Probably not a symbolic link object or access issue.

### <a id="methods-tostring"/>**ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)
