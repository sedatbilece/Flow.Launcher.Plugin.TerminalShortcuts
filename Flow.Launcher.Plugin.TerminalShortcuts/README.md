# Flow Launcher – Terminal Shortcuts Plugin

A Flow Launcher plugin that opens a terminal in a configured directory and optionally runs a command when an abbreviation is typed.

> Turkish documentation: [README_TR.md](README_TR.md)

## Installation

```powershell
.\build.ps1
```

The script builds the project, stops Flow Launcher, copies the plugin to the plugins folder, and restarts Flow Launcher.

## Usage

Type `t` in Flow Launcher (default keyword):

| Input | Result |
|---|---|
| `t` | Lists all shortcuts |
| `t prj` | Filters shortcuts matching "prj" in name or abbreviation |
| `t reload` | Reloads `shortcuts.json` without restarting |

Pressing Enter on a result opens the terminal in the configured directory and runs the command if one is set.

## Configuration

After deploying, the config file is located at:

```
%APPDATA%\FlowLauncher\Plugins\Flow.Launcher.Plugin.TerminalShortcuts\shortcuts.json
```

### shortcuts.json format

```json
[
  {
    "Name": "Project API",
    "Abbreviation": "api",
    "Directory": "C:\\Projects\\MyApi",
    "Command": "dotnet run",
    "Terminal": "wt"
  }
]
```

### Field reference

| Field | Description | Required |
|---|---|---|
| `Name` | Display title and search text in Flow Launcher | Yes |
| `Abbreviation` | Short search keyword (e.g. `api`, `prj`) | No |
| `Directory` | Directory where the terminal will open | Yes |
| `Command` | Command to run automatically in that directory | No |
| `Terminal` | Terminal application to use | No (default: `wt`) |

### Terminal options

| Value | Application |
|---|---|
| `wt` | Windows Terminal (default) |
| `powershell` | Windows PowerShell |
| `cmd` | Command Prompt |

If `Command` is empty, the terminal opens in the specified directory without running any command.

### Example shortcuts.json

```json
[
  {
    "Name": "Example – Project",
    "Abbreviation": "prj",
    "Directory": "C:\\Projects\\MyProject",
    "Command": "npm start",
    "Terminal": "wt"
  },
  {
    "Name": "Example – Home (terminal only)",
    "Abbreviation": "home",
    "Directory": "C:\\Users\\sedat.bilece",
    "Command": "",
    "Terminal": "powershell"
  }
]
```

**"Example – Project":** Shown when typing `t prj`. Opens Windows Terminal in `C:\Projects\MyProject` and runs `npm start`.

**"Example – Home":** Shown when typing `t home`. Opens PowerShell in `C:\Users\sedat.bilece` without running any command.

### After editing the config

Type `t reload` in Flow Launcher and press Enter. No need to restart the plugin or Flow Launcher.
