// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

using Nefarius.Utilities.DeviceManagement.PnP;
using Nefarius.Utilities.NtDll.Handles;
using Nefarius.Utilities.NtDll.Objects;

try
{
    Random random = new();

    Process randomProcess = Process.GetProcesses()
        .OrderBy(p => random.Next())
        .First(p => p.Id != 4 /* requires elevation */);

    string? handleName = SystemHandle.AllHandles
        .FirstOrDefault(h => h.ProcessId == randomProcess.Id && !string.IsNullOrEmpty(h.Name))?.Name;

    Console.WriteLine(handleName);
}
catch (SystemHandleException shex)
{
    Console.WriteLine(shex.Message);
}

foreach (NtDirectoryObject globalObject in NtDirectoryObject.GlobalObjects.Where(o => o.IsSymbolicLink))
{
    NtSymbolicLinkObject? target = NtSymbolicLinkObject.GetLinkTarget(globalObject);

    if (target is not null && globalObject.Name.Contains("USB"))
    {
        Console.WriteLine($"{globalObject} -> {target}");

        try
        {
            string? device = PnPDevice.GetInstanceIdFromInterfaceId(globalObject.Path);

            Console.WriteLine($"\tEnumerated device {device}");
        }
        catch
        {
            // ignored
        }
    }
}