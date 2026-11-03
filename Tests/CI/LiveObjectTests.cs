using System.Diagnostics;

using Nefarius.Utilities.NtDll.Handles;
using Nefarius.Utilities.NtDll.Objects;
using Nefarius.Utilities.NtDll.Util;

namespace Tests.CI;

[TestFixture]
[Category(TestCategories.CI)]
internal class LiveObjectTests
{
    [Test]
    public void GlobalObjects_ContainsSymbolicLinks()
    {
        IReadOnlyCollection<NtDirectoryObject> objects = NtDirectoryObject.GlobalObjects;

        Assert.That(objects, Is.Not.Empty);
        Assert.That(objects.Any(obj => obj.IsSymbolicLink), Is.True);
    }

    [Test]
    public void GetLinkTarget_DriveC_ResolvesToDevicePath()
    {
        NtSymbolicLinkObject? target = NtSymbolicLinkObject.GetLinkTarget(@"\GLOBAL??\C:");

        Assert.That(target, Is.Not.Null);
        Assert.That(target!.LinkTarget, Does.StartWith(@"\Device\"));
    }

    [Test]
    [CancelAfter(30_000)]
    public void GetSystemHandles_CurrentProcess_IncludesOpenedFile()
    {
        string path = Path.GetTempFileName();

        try
        {
            using FileStream stream = File.OpenRead(path);
            string fileName = Path.GetFileName(path);
            IReadOnlyCollection<SystemHandle> handles = Process.GetCurrentProcess().GetSystemHandles();

            Assert.That(handles, Is.Not.Empty);

            bool found = false;
            foreach (SystemHandle handle in handles)
            {
                string name;
                try
                {
                    name = handle.Name;
                }
                catch (Exception)
                {
                    // Some objects reject name queries or return a length the wrapper does not accept.
                    continue;
                }

                if (name.Contains(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            Assert.That(found, Is.True, $"Expected an open handle whose name contains {fileName}.");
        }
        finally
        {
            File.Delete(path);
        }
    }
}
