# Release zip oluşturur.
$project   = "Flow.Launcher.Plugin.TerminalShortcuts"
$publishDir = "$project\bin\Release\win-x64\publish"
$zipName   = "TerminalShortcuts.zip"

dotnet publish $project -c Release -r win-x64 --no-self-contained
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

if (Test-Path $zipName) { Remove-Item $zipName }
Compress-Archive -Path "$publishDir\*" -DestinationPath $zipName
Write-Host "Created $zipName" -ForegroundColor Green
