// See https://aka.ms/new-console-template for more information

using Nefarius.Utilities.DeviceManagement.PnP;
using Nefarius.Utilities.NtDll.Objects;

foreach (NtDirectoryObject globalObject in NtDirectoryObject.GlobalObjects.Where(o => o.IsSymbolicLink))
{
    NtSymbolicLinkObject? target = NtSymbolicLinkObject.GetLinkTarget(globalObject.FullName);

    if (target is not null && globalObject.Name.Contains("USB"))
    {
        Console.WriteLine($"{globalObject} -> {target}");

        try
        {
            var device = PnPDevice.GetInstanceIdFromInterfaceId(globalObject.Path);
        }
        catch
        {
            continue;
        }

        var t = 0;
    }
}

Console.ReadKey();