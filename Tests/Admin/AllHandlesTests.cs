using Nefarius.Utilities.NtDll.Handles;

namespace Tests.Admin;

[TestFixture]
[Category(TestCategories.Admin)]
[Explicit("Enumerates every handle on the system and needs an elevated process.")]
internal class AllHandlesTests
{
    [Test]
    public void AllHandles_IncludesOtherProcesses()
    {
        IReadOnlyCollection<SystemHandle> handles = SystemHandle.AllHandles;
        uint currentProcessId = (uint)Environment.ProcessId;

        Assert.That(handles, Is.Not.Empty);
        Assert.That(handles.Any(handle => handle.ProcessId != currentProcessId), Is.True);
    }
}
