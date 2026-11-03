using Nefarius.Utilities.NtDll.Objects;

namespace Tests.Unit;

[TestFixture]
[Category(TestCategories.Unit)]
internal class NtDirectoryObjectTests
{
    [Test]
    public void FullName_And_Path_IncludeNamespace()
    {
        NtDirectoryObject obj = new()
        {
            Name = "C:",
            TypeName = "SymbolicLink"
        };

        Assert.That(NtDirectoryObject.GlobalPrefix, Is.EqualTo(@"\GLOBAL??"));
        Assert.That(obj.FullName, Is.EqualTo(@"\GLOBAL??\C:"));
        Assert.That(obj.Path, Is.EqualTo(@"\\?\C:"));
        Assert.That(obj.IsSymbolicLink, Is.True);
        Assert.That(obj.ToString(), Is.EqualTo("C: (SymbolicLink)"));
    }

    [Test]
    public void IsSymbolicLink_IsFalseForOtherTypes()
    {
        NtDirectoryObject obj = new()
        {
            Name = "BaseNamedObjects",
            TypeName = "Directory"
        };

        Assert.That(obj.IsSymbolicLink, Is.False);
    }
}
