# Builds the plugin, copies it to the Flow Launcher plugins folder, and restarts Flow Launcher.

$pluginName = "TerminalShortcuts"
$project    = "Flow.Launcher.Plugin.TerminalShortcuts"
$publishDir = "$project\bin\Release\win-x64\publish"
$flPlugins  = "$env:APPDATA\FlowLauncher\Plugins"
$target     = "$flPlugins\Flow.Launcher.Plugin.$pluginName"

Write-Host "Building..." -ForegroundColor Cyan
dotnet publish $project -c Release -r win-x64 --no-self-contained
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Stopping Flow Launcher..." -ForegroundColor Yellow
Get-Process "Flow.Launcher" -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Milliseconds 800

Write-Host "Copying to $target ..." -ForegroundColor Cyan

# Back up existing shortcuts.json if present
$existingShortcuts = "$target\shortcuts.json"
$backupShortcuts   = "$env:TEMP\shortcuts.json.bak"
if (Test-Path $existingShortcuts) {
    Copy-Item $existingShortcuts -Destination $backupShortcuts -Force
}

if (Test-Path $target) { Remove-Item $target -Recurse -Force }
Copy-Item $publishDir -Destination $target -Recurse

# Restore shortcuts.json if it was backed up; otherwise the example file from publish is used
if (Test-Path $backupShortcuts) {
    Copy-Item $backupShortcuts -Destination $existingShortcuts -Force
    Remove-Item $backupShortcuts -Force
    Write-Host "shortcuts.json preserved." -ForegroundColor DarkGray
} else {
    Write-Host "No existing shortcuts.json found, using example file." -ForegroundColor DarkGray
}

Write-Host "Starting Flow Launcher..." -ForegroundColor Green
Start-Process "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

Write-Host "Done." -ForegroundColor Green
