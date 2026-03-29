using System.Diagnostics;

namespace XPaint.Cheats;

internal static class CheatDiagnostics
{
    public static void Info(string source, string message) =>
        Debug.WriteLine($"[INFO] [{source}] {message}");

    public static void Warning(string source, string message) =>
        Debug.WriteLine($"[WARN] [{source}] {message}");

    public static void Error(string source, string message) =>
        Debug.WriteLine($"[ERROR] [{source}] {message}");
}
