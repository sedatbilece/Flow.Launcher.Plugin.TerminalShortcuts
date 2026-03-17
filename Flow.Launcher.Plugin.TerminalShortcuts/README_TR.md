# Flow Launcher – Terminal Shortcuts Plugin

Flow Launcher'da bir kısaltma yazıldığında, belirlenen dizinde terminal açan ve isteğe bağlı komut çalıştıran plugin.

> İngilizce dokümantasyon: [README.md](README.md)

## Kurulum

```powershell
.\build.ps1
```

Script şunları yapar: projeyi derler → Flow Launcher'ı kapatır → plugin klasörüne kopyalar → Flow Launcher'ı yeniden başlatır.

## Kullanım

Flow Launcher'da `t` yazın (varsayılan keyword):

| Yazılan | Sonuç |
|---|---|
| `t` | Tüm shortcut'ları listeler |
| `t prj` | İsim veya kısaltmada "prj" geçenleri filtreler |
| `t reload` | `shortcuts.json`'u yeniden yükler (restart gerekmez) |

Bir sonuca Enter bastığınızda terminal açılır, ilgili dizinde konumlanır ve varsa komut otomatik çalıştırılır.

## Konfigürasyon

Plugin deploy edildikten sonra config dosyası burada bulunur:

```
%APPDATA%\FlowLauncher\Plugins\Flow.Launcher.Plugin.TerminalShortcuts\shortcuts.json
```

### shortcuts.json formatı

```json
[
  {
    "Name": "Proje API",
    "Abbreviation": "api",
    "Directory": "C:\\Projects\\MyApi",
    "Command": "dotnet run",
    "Terminal": "wt"
  }
]
```

### Alan açıklamaları

| Alan | Açıklama | Zorunlu |
|---|---|---|
| `Name` | Flow Launcher'da görünen başlık ve arama metni | Evet |
| `Abbreviation` | Kısa arama kısaltması (örn. `api`, `prj`) | Hayır |
| `Directory` | Terminalin açılacağı dizin | Evet |
| `Command` | Dizinde otomatik çalıştırılacak komut | Hayır |
| `Terminal` | Kullanılacak terminal uygulaması | Hayır (varsayılan: `wt`) |

### Terminal seçenekleri

| Değer | Uygulama |
|---|---|
| `wt` | Windows Terminal (varsayılan) |
| `powershell` | Windows PowerShell |
| `cmd` | Komut İstemi |

`Command` boş bırakılırsa terminal yalnızca belirtilen dizinde açılır, herhangi bir komut çalıştırılmaz.

### Örnek shortcuts.json

```json
[
  {
    "Name": "Örnek – Proje",
    "Abbreviation": "prj",
    "Directory": "C:\\Projects\\MyProject",
    "Command": "npm start",
    "Terminal": "wt"
  },
  {
    "Name": "Örnek – Home (sadece terminal)",
    "Abbreviation": "home",
    "Directory": "C:\\Users\\sedat.bilece",
    "Command": "",
    "Terminal": "powershell"
  }
]
```

**"Örnek – Proje":** `t prj` yazınca listelenir. Seçilince Windows Terminal `C:\Projects\MyProject` dizininde açılır ve `npm start` komutu çalıştırılır.

**"Örnek – Home":** `t home` yazınca listelenir. Seçilince PowerShell `C:\Users\sedat.bilece` dizininde açılır, komut çalıştırılmaz.

### Config değişikliği sonrası

Dosyayı düzenledikten sonra Flow Launcher'da `t reload` yazıp Enter'a basmanız yeterlidir. Plugin veya Flow Launcher'ı yeniden başlatmaya gerek yoktur.
