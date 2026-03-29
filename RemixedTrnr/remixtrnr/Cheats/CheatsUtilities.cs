using System.Windows;
using System.Collections.Concurrent;
using XPaint.Models;
using MahApps.Metro.Controls;
using Memory;
using XPaint.Services;
using static XPaint.Resources.Memory;

namespace XPaint.Cheats;

public class CheatsUtilities
{
    private static readonly ConcurrentDictionary<string, nuint> s_cachedAobHits = new(StringComparer.Ordinal);
    private static readonly (string Name, bool Readable, bool Writable, bool Executable, bool Mapped)[] s_scanStrategies =
    [
        ("executable", false, false, true, false),
        ("writable", false, true, false, false),
        ("readable", true, false, false, false),
        ("fallback", true, true, true, true)
    ];

    protected static async Task<nuint> SmartAobScan(string search, UIntPtr? start = null, UIntPtr? end = null, bool forceRefresh = false)
    {
        search = Memory.Utils.NormalizeSignature(search);
        var processMainModule = GetInstance().MProc.Process.MainModule;
        if (processMainModule == null)
        {
            CheatDiagnostics.Warning(nameof(CheatsUtilities), "Main module was null while resolving an AOB scan.");
            return 0;
        }

        var minRange = (long)processMainModule.BaseAddress;
        var maxRange = minRange + processMainModule.ModuleMemorySize;

        if (start != null)
        {
            minRange = (long)start;
        }
        
        if (end != null)
        {
            maxRange = (long)end;
        }

        if (!forceRefresh && s_cachedAobHits.TryGetValue(search, out var cachedHit))
        {
            if (cachedHit >= (nuint)minRange && cachedHit < (nuint)maxRange)
            {
                if (IsSignatureStillValid(cachedHit, search))
                {
                    CheatDiagnostics.Info(nameof(CheatsUtilities), $"Using cached AOB hit 0x{cachedHit:X}.");
                    return cachedHit;
                }

                s_cachedAobHits.TryRemove(search, out _);
                CheatDiagnostics.Warning(nameof(CheatsUtilities), $"Discarded stale cached AOB hit 0x{cachedHit:X}.");
            }
        }

        var resolved = await SmartAobScanInternal(search, minRange, maxRange);
        if (resolved > 0)
        {
            s_cachedAobHits[search] = resolved;
            CheatDiagnostics.Info(nameof(CheatsUtilities), $"Resolved AOB hit 0x{resolved:X} in range 0x{minRange:X}-0x{maxRange:X}.");
        }
        else
        {
            s_cachedAobHits.TryRemove(search, out _);
            CheatDiagnostics.Warning(nameof(CheatsUtilities), $"Failed to resolve AOB in range 0x{minRange:X}-0x{maxRange:X}.");
        }

        return resolved;
    }

    protected static Task<nuint> RefreshAobScan(string search, UIntPtr? start = null, UIntPtr? end = null)
    {
        return SmartAobScan(search, start, end, true);
    }

    private static async Task<nuint> SmartAobScanInternal(string search, long minRange, long maxRange)
    {
        var mem = GetInstance();
        foreach (var strategy in s_scanStrategies)
        {
            var result = (await mem.AoBScan(minRange, maxRange, search, strategy.Readable, strategy.Writable, strategy.Executable, strategy.Mapped, 1)).FirstOrDefault();
            if (result > 0)
            {
                CheatDiagnostics.Info(nameof(CheatsUtilities), $"AOB scan matched using {strategy.Name} strategy at 0x{result:X}.");
                return result;
            }

            CheatDiagnostics.Info(nameof(CheatsUtilities), $"AOB scan did not match using {strategy.Name} strategy.");
        }

        return 0;
    }

    private static bool IsSignatureStillValid(nuint address, string search)
    {
        var pattern = Memory.Utils.ParseSig(search, out var mask);
        if (pattern.Length == 0)
        {
            return false;
        }

        var bytes = GetInstance().ReadArrayMemory<byte>(address, pattern.Length);
        return bytes.Length == pattern.Length && Memory.Utils.IsMatchAt(bytes, 0, pattern, mask);
    }
    
    protected static void ShowError(string feature, string sig)
    {
        CheatDiagnostics.Error(feature, sig);
        MessageBox.Show(
            $"Address for this feature wasn't found!\nPlease try to activate the cheat again or try to restart the game and the tool.\n\nFeature: {feature}\nSignature: {sig}\n\nVersion: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}\nGame: {GameVerPlat.GetInstance().Name}\nGame Version: {GameVerPlat.GetInstance().Update}\nPlatform: {GameVerPlat.GetInstance().Platform}",
            $"xpaint - Error", 0, MessageBoxImage.Error);
    }

    protected static void Free(UIntPtr address)
    {
        if (address == 0) return;
        var handle = GetInstance().MProc.Handle;
        Imps.VirtualFreeEx(handle, address, 0, Imps.MemRelease);
    }
    
    protected static byte[] CalculateDetour(nuint address, nuint target, int replaceCount)
    {
        var detourBytes = new byte[replaceCount];
        detourBytes[0] = 0xE9;
        BitConverter.GetBytes((int)((long)target - (long)address - 5)).CopyTo(detourBytes, 1);
        
        for (var i = 5; i < detourBytes.Length; i++)
        {
            detourBytes[i] = 0x90;
        }

        return detourBytes;
    }
}
