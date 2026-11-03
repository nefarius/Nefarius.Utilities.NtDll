# NtDirectoryObject

Namespace: Nefarius.Utilities.NtDll.Objects

A generic NT object.

```csharp
public sealed class NtDirectoryObject
```

Inheritance [Object](https://learn.microsoft.com/dotnet/api/system.object) → [NtDirectoryObject](./nefarius.utilities.ntdll.objects.ntdirectoryobject.md)

## Properties

### <a id="properties-fullname"/>**FullName**

Gets the full object path including the namespace.

```csharp
public string FullName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-globalobjects"/>**GlobalObjects**

Gets all global objects.

```csharp
public static IReadOnlyCollection<NtDirectoryObject> GlobalObjects { get; }
```

#### Property Value

[IReadOnlyCollection](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1)<[NtDirectoryObject](./nefarius.utilities.ntdll.objects.ntdirectoryobject.md)><br>

### <a id="properties-globalprefix"/>**GlobalPrefix**

Gets the global objects namespace prefix.

```csharp
public static string GlobalPrefix { get; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-issymboliclink"/>**IsSymbolicLink**

True if this object is a symbolic link, false otherwise.

```csharp
public bool IsSymbolicLink { get; }
```

#### Property Value

[Boolean](https://learn.microsoft.com/dotnet/api/system.boolean)<br>

### <a id="properties-name"/>**Name**

Gets the object name without the namespace prefix.

```csharp
public string Name { get; internal set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-path"/>**Path**

Gets the symbolic link path in a format for CreateFile, CM APIs etc.

```csharp
public string Path { get; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

### <a id="properties-typename"/>**TypeName**

Gets the object type.

```csharp
public string TypeName { get; internal set; }
```

#### Property Value

[String](https://learn.microsoft.com/dotnet/api/system.string)<br>

## Constructors

### <a id="constructors-.ctor"/>**NtDirectoryObject()**

```csharp
public NtDirectoryObject()
```

## Methods

### <a id="methods-tostring"/>**ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://learn.microsoft.com/dotnet/api/system.string)
