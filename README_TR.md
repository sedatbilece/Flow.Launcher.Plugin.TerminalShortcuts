# Flow Launcher – Terminal Shortcuts Plugin

Flow Launcher'da bir kısaltma yazıldığında, belirlenen dizinde terminal açan ve isteğe bağlı komut çalıştıran plugin.

> İngilizce dokümantasyon: [README.md](README.md)

## Build & Deploy

```powershell
.\build.ps1
```

## Proje Yapısı

```
Flow.Launcher.Plugin.TerminalShortcuts/   ← plugin kaynak kodu
    Main.cs
    Shortcut.cs
    plugin.json
    shortcuts.json                        ← kullanıcı konfigürasyonu
    README.md
    README_TR.md
build.ps1                                 ← derle ve Flow Launcher'a deploy et
release.ps1                               ← dağıtılabilir .zip oluşturur
```

Tam dokümantasyon için bkz. [Flow.Launcher.Plugin.TerminalShortcuts/README_TR.md](Flow.Launcher.Plugin.TerminalShortcuts/README_TR.md).
