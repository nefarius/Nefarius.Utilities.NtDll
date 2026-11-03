# Testing

This repository mixes hermetic unit tests, Windows CI integration tests against the live OS, and an Explicit admin suite that enumerates every handle on the system.

## Quick start (safe default)

On Windows:

```powershell
dotnet test Tests/Tests.csproj -c Release --filter "Category=Unit|Category=CI"
```

A bare `dotnet test` also skips `[Explicit]` tests, so Admin cases will not run unless selected.

## Categories

| Category | Purpose | Runs on GitHub-hosted CI |
|----------|---------|--------------------------|
| `Unit` | Path formatting, marshalling, and exception messages. No live OS calls | Yes (`windows-latest`) |
| `CI` | Live Windows, read-only queries (`\GLOBAL??`, `C:` symbolic link, current-process handles) | Yes (`windows-latest`) |
| `Admin` | Enumerates every handle on the system, including other processes | No — Explicit |

`GetSystemHandles` and `SystemHandle.AllHandles` both call `NtQuerySystemInformation`. GitHub `windows-latest` runs elevated, so the CI handle test can see the current process. Run the Admin suite yourself when you want the cross-process check.

## Admin suite

Run elevated:

```powershell
dotnet test Tests/Tests.csproj -c Release --filter "Category=Admin"
```

This walks the system handle table. It does not change system state, but it is slow and some object-name queries can stall, so it stays out of CI.

## CI layout

- **Ubuntu `build` job** — multi-TFM compile gate.
- **Windows `test-windows` job** — `Category=Unit|Category=CI` with TRX + coverage artifacts.
- **NuGet publish** — the same Windows test filter must pass before pack/push.
