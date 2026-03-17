# Flow Launcher – Terminal Shortcuts Plugin

A Flow Launcher plugin that opens a terminal in a configured directory and optionally runs a command when an abbreviation is typed.


## Build & Deploy

```powershell
.\build.ps1
```


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
```

