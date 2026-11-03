using Windows.Win32.Foundation;

using Nefarius.Utilities.NtDll.Handles;
using Nefarius.Utilities.NtDll.Objects;

namespace Tests.Unit;

[TestFixture]
[Category(TestCategories.Unit)]
internal class ExceptionMessageTests
{
    [Test]
    [Platform("Win")]
    public void NtObjectException_MessageIncludesWin32Text()
    {
        NtObjectException exception = new("NtQueryObject failed.", NTSTATUS.STATUS_ACCESS_DENIED);

        Assert.That(exception.ErrorCode, Is.EqualTo(5));
        Assert.That(exception.Message, Does.StartWith("NtQueryObject failed."));
        Assert.That(exception.Message, Does.Contain("Access is denied"));
    }

    [Test]
    [Platform("Win")]
    public void NtDirectoryObjectException_MessageIncludesWin32Text()
    {
        NtDirectoryObjectException exception = new("NtOpenDirectoryObject failed.", NTSTATUS.STATUS_ACCESS_DENIED);

        Assert.That(exception.ErrorCode, Is.EqualTo(5));
        Assert.That(exception.Message, Does.StartWith("NtOpenDirectoryObject failed."));
        Assert.That(exception.Message, Does.Contain("Access is denied"));
    }

    [Test]
    [Platform("Win")]
    public void NtSymbolicLinkObjectException_MessageIncludesWin32Text()
    {
        NtSymbolicLinkObjectException exception =
            new("NtOpenSymbolicLinkObject failed.", NTSTATUS.STATUS_ACCESS_DENIED);

        Assert.That(exception.ErrorCode, Is.EqualTo(5));
        Assert.That(exception.Message, Does.StartWith("NtOpenSymbolicLinkObject failed."));
        Assert.That(exception.Message, Does.Contain("Access is denied"));
    }

    [Test]
    [Platform("Win")]
    public void SystemHandleException_FromNtStatus_MessageIncludesWin32Text()
    {
        SystemHandleException exception = new("NtDuplicateObject failed", NTSTATUS.STATUS_ACCESS_DENIED);

        Assert.That(exception.ErrorCode, Is.EqualTo(5));
        Assert.That(exception.Message, Does.StartWith("NtDuplicateObject failed"));
        Assert.That(exception.Message, Does.Contain("Access is denied"));
    }

    [Test]
    public void SystemHandleException_FromWin32Error_KeepsErrorCode()
    {
        SystemHandleException exception = new("OpenProcess failed", WIN32_ERROR.ERROR_ACCESS_DENIED);

        Assert.That(exception.ErrorCode, Is.EqualTo(5));
        Assert.That(exception.Message, Does.StartWith("OpenProcess failed - "));
        if (OperatingSystem.IsWindows())
        {
            Assert.That(exception.Message, Does.Contain("Access is denied"));
        }
    }
}
