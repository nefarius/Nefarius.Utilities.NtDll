// See https://aka.ms/new-console-template for more information

using Nefarius.Utilities.DeviceManagement.PnP;
using Nefarius.Utilities.NtDll.Handles;
using Nefarius.Utilities.NtDll.Objects;

SystemHandle.Test();

foreach (NtDirectoryObject globalObject in NtDirectoryObject.GlobalObjects.Where(o => o.IsSymbolicLink))
{
    NtSymbolicLinkObject? target = NtSymbolicLinkObject.GetLinkTarget(globalObject);

    if (target is not null && globalObject.Name.Contains("USB"))
    {
        Console.WriteLine($"{globalObject} -> {target}");

        try
        {
            var device = PnPDevice.GetInstanceIdFromInterfaceId(globalObject.Path);
            
            Console.WriteLine($"Enumerated device {device}");
        }
        catch
        {
            continue;
        }
    }
}
