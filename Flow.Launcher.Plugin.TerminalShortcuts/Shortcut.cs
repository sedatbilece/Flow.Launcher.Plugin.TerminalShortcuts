namespace Flow.Launcher.Plugin.TerminalShortcuts;

public class Shortcut
{
    /// <summary>Display name and search text shown in Flow Launcher.</summary>
    public string Name { get; set; } = "";

    /// <summary>Short search abbreviation (optional). If empty, Name is used for matching.</summary>
    public string Abbreviation { get; set; } = "";

    /// <summary>Directory where the terminal will open.</summary>
    public string Directory { get; set; } = "";

    /// <summary>Command to run automatically in the directory. If empty, only the terminal is opened.</summary>
    public string Command { get; set; } = "";

    /// <summary>Terminal to use: "wt" (Windows Terminal), "powershell", "cmd". Default: "wt"</summary>
    public string Terminal { get; set; } = "wt";
}
