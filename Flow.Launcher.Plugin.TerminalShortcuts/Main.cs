using System.Diagnostics;
using System.Text.Json;

namespace Flow.Launcher.Plugin.TerminalShortcuts;

public class Main : IPlugin
{
    private PluginInitContext _context = null!;
    private List<Shortcut> _shortcuts = new();
    private string _settingsPath = "";

    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };

    public void Init(PluginInitContext context)
    {
        _context = context;
        _settingsPath = Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "shortcuts.json");
        LoadSettings();
    }

    public List<Result> Query(Query query)
    {
        var search = query.Search.Trim();

        // "reload" yazılınca ayarları yeniden yükle
        if (search.Equals("reload", StringComparison.OrdinalIgnoreCase))
        {
            return new List<Result>
            {
                new Result
                {
                    Title = "Reload shortcuts.json",
                    SubTitle = _settingsPath,
                    IcoPath = "icon.png",
                    Action = _ => { LoadSettings(); return true; }
                }
            };
        }

        var matches = _shortcuts
            .Where(s => IsMatch(s, search))
            .Select(s => new Result
            {
                Title = s.Name,
                SubTitle = BuildSubtitle(s),
                IcoPath = "icon.png",
                Action = _ => { OpenTerminal(s); return true; }
            })
            .ToList();

        return matches;
    }

    // ─── helpers ────────────────────────────────────────────────────────────

    private static bool IsMatch(Shortcut s, string search)
    {
        if (string.IsNullOrEmpty(search)) return true;
        return s.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
            || s.Abbreviation.Contains(search, StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildSubtitle(Shortcut s)
    {
        var parts = new List<string>();
        if (!string.IsNullOrEmpty(s.Directory)) parts.Add(s.Directory);
        if (!string.IsNullOrEmpty(s.Command))   parts.Add($"> {s.Command}");
        parts.Add($"[{s.Terminal}]");
        return string.Join("  ", parts);
    }

    private void LoadSettings()
    {
        if (!File.Exists(_settingsPath))
        {
            CreateDefaultSettings();
            return;
        }

        try
        {
            var json = File.ReadAllText(_settingsPath);
            _shortcuts = JsonSerializer.Deserialize<List<Shortcut>>(json, JsonOpts) ?? new List<Shortcut>();
        }
        catch (Exception ex)
        {
            _context.API.ShowMsg("TerminalShortcuts", $"shortcuts.json okunamadı: {ex.Message}");
            _shortcuts = new List<Shortcut>();
        }
    }

    private void CreateDefaultSettings()
    {
        _shortcuts = new List<Shortcut>
        {
            new Shortcut
            {
                Name = "Örnek – Proje",
                Abbreviation = "prj",
                Directory = @"C:\Projects\MyProject",
                Command = "npm start",
                Terminal = "wt"
            },
            new Shortcut
            {
                Name = "Örnek – Home (sadece terminal)",
                Abbreviation = "home",
                Directory = @"C:\Users\" + Environment.UserName,
                Command = "",
                Terminal = "powershell"
            }
        };

        File.WriteAllText(_settingsPath, JsonSerializer.Serialize(_shortcuts, JsonOpts));
    }

    private void OpenTerminal(Shortcut s)
    {
        var dir     = s.Directory;
        var cmd     = s.Command;
        var hasCmd  = !string.IsNullOrWhiteSpace(cmd);
        var terminal = (s.Terminal ?? "wt").ToLowerInvariant();

        ProcessStartInfo psi = terminal switch
        {
            "wt" => BuildWt(dir, cmd, hasCmd),
            "powershell" => BuildPowershell(dir, cmd, hasCmd),
            _ => BuildCmd(dir, cmd, hasCmd)   // "cmd" veya bilinmeyen
        };

        try
        {
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            _context.API.ShowMsg("TerminalShortcuts", $"Terminal açılamadı: {ex.Message}");
        }
    }

    // wt -d "DIR" [powershell -NoExit -Command "CMD"]
    private static ProcessStartInfo BuildWt(string dir, string cmd, bool hasCmd)
    {
        var args = hasCmd
            ? $"-d \"{dir}\" powershell.exe -NoExit -Command \"{EscapePs(cmd)}\""
            : $"-d \"{dir}\"";

        return new ProcessStartInfo { FileName = "wt", Arguments = args, UseShellExecute = true };
    }

    // powershell -NoExit -Command "Set-Location 'DIR'; CMD"
    private static ProcessStartInfo BuildPowershell(string dir, string cmd, bool hasCmd)
    {
        var psCmd = hasCmd
            ? $"Set-Location '{dir}'; {cmd}"
            : $"Set-Location '{dir}'";

        return new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoExit -Command \"{EscapePs(psCmd)}\"",
            UseShellExecute = true
        };
    }

    // cmd /k "cd /d DIR && CMD"
    private static ProcessStartInfo BuildCmd(string dir, string cmd, bool hasCmd)
    {
        var inner = hasCmd
            ? $"cd /d \"{dir}\" && {cmd}"
            : $"cd /d \"{dir}\"";

        return new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/k \"{inner}\"",
            UseShellExecute = true
        };
    }

    // PowerShell argümanı içindeki çift tırnak kaçırma
    private static string EscapePs(string s) => s.Replace("\"", "`\"");
}
