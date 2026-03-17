namespace Flow.Launcher.Plugin.TerminalShortcuts;

public class Shortcut
{
    /// <summary>Gösterim adı ve arama için kullanılır.</summary>
    public string Name { get; set; } = "";

    /// <summary>Kısa arama kısaltması (isteğe bağlı). Boşsa Name üzerinden aranır.</summary>
    public string Abbreviation { get; set; } = "";

    /// <summary>Terminalin açılacağı dizin.</summary>
    public string Directory { get; set; } = "";

    /// <summary>Dizinde otomatik çalıştırılacak komut. Boş bırakılırsa sadece terminal açılır.</summary>
    public string Command { get; set; } = "";

    /// <summary>Kullanılacak terminal: "wt" (Windows Terminal), "powershell", "cmd". Varsayılan: "wt"</summary>
    public string Terminal { get; set; } = "wt";
}
