# Plugini derler, Flow Launcher plugins klasörüne kopyalar ve Flow Launcher'ı yeniden başlatır.

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

# Mevcut shortcuts.json varsa yedekle
$existingShortcuts = "$target\shortcuts.json"
$backupShortcuts   = "$env:TEMP\shortcuts.json.bak"
if (Test-Path $existingShortcuts) {
    Copy-Item $existingShortcuts -Destination $backupShortcuts -Force
}

if (Test-Path $target) { Remove-Item $target -Recurse -Force }
Copy-Item $publishDir -Destination $target -Recurse

# shortcuts.json'u geri yükle (varsa); yoksa publish'teki örnek kalır
if (Test-Path $backupShortcuts) {
    Copy-Item $backupShortcuts -Destination $existingShortcuts -Force
    Remove-Item $backupShortcuts -Force
    Write-Host "shortcuts.json korundu." -ForegroundColor DarkGray
} else {
    Write-Host "shortcuts.json bulunamadı, örnek dosya kullanılıyor." -ForegroundColor DarkGray
}

Write-Host "Starting Flow Launcher..." -ForegroundColor Green
Start-Process "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

Write-Host "Done." -ForegroundColor Green
