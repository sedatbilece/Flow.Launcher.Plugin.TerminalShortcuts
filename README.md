# Flow Launcher – Terminal Shortcuts Plugin

A Flow Launcher plugin that opens a terminal in a configured directory and optionally runs a command when an abbreviation is typed.

> Turkish documentation: [README_TR.md](README_TR.md)

## Build & Deploy

```powershell
.\build.ps1
```

## Project Structure

```
Flow.Launcher.Plugin.TerminalShortcuts/   ← plugin source
    Main.cs
    Shortcut.cs
    plugin.json
    shortcuts.json                        ← user config
    README.md
    README_TR.md
build.ps1                                 ← build & deploy to Flow Launcher
release.ps1                               ← creates a distributable .zip
```

For full documentation see [Flow.Launcher.Plugin.TerminalShortcuts/README.md](Flow.Launcher.Plugin.TerminalShortcuts/README.md).
