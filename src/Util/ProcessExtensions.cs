using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

using Nefarius.Utilities.NtDll.Handles;

namespace Nefarius.Utilities.NtDll.Util;

/// <summary>
///     Extends <see cref="Process" /> type.
/// </summary>
[UsedImplicitly]
public static class ProcessExtensions
{
    /// <summary>
    ///     Gets all <see cref="SystemHandle" />s associated to <paramref name="process" />.
    /// </summary>
    /// <param name="process">The <see cref="Process" /> to enumerate.</param>
    /// <returns>The list of handles.</returns>
    [UsedImplicitly]
    public static IReadOnlyCollection<SystemHandle> GetSystemHandles(this Process process)
    {
        return SystemHandle.AllHandles
            .Where(h => h.ProcessId == process.Id)
            .ToList()
            .AsReadOnly();
    }
}