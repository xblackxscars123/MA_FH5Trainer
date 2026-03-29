using System;
using System.Collections.Generic;

namespace Memory;

public static class Utils
{
    public static string NormalizeSignature(string sig)
    {
        sig = sig.Replace('*', '?').Trim();
        while (sig.EndsWith(" ?", StringComparison.Ordinal) || sig.EndsWith(" ??", StringComparison.Ordinal))
        {
            if (sig.EndsWith(" ??", StringComparison.Ordinal))
            {
                sig = sig[..^3];
            }

            if (sig.EndsWith(" ?", StringComparison.Ordinal))
            {
                sig = sig[..^2];
            }
        }

        var tokens = sig.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        for (var i = 0; i < tokens.Length; i++)
        {
            if (tokens[i].Length > 0 && tokens[i][0] == '?')
            {
                tokens[i] = "??";
            }
        }

        return string.Join(" ", tokens);
    }

    public static byte[] ParseSig(string sig, out byte[] mask)
    {
        sig = NormalizeSignature(sig);
        var stringByteArray = sig.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var sigPattern = new byte[stringByteArray.Length];
        mask = new byte[stringByteArray.Length];

        for (var i = 0; i < stringByteArray.Length; i++)
        {
            var ba = stringByteArray[i];

            if (ba == "??" || (ba.Length == 1 && ba == "?"))
            {
                mask[i] = 0x00;
                stringByteArray[i] = "0x00";
            }
            else if (char.IsLetterOrDigit(ba[0]) && ba[1] == '?')
            {
                mask[i] = 0xF0;
                stringByteArray[i] = ba[0] + "0";
            }
            else if (char.IsLetterOrDigit(ba[1]) && ba[0] == '?')
            {
                mask[i] = 0x0F;
                stringByteArray[i] = "0" + ba[1];
            }
            else
            {
                mask[i] = 0xFF;
            }
        }

        for (var i = 0; i < stringByteArray.Length; i++)
        {
            const int hexBase = 16;
            sigPattern[i] = (byte)(Convert.ToByte(stringByteArray[i], hexBase) & mask[i]);
        }
        
        return sigPattern;
    }

    public static List<int> FindPatternOffsets(ReadOnlySpan<byte> data, string sig, int resultLimit = -1)
    {
        var pattern = ParseSig(sig, out var mask);
        return FindPatternOffsets(data, pattern, mask, resultLimit);
    }

    public static List<int> FindPatternOffsets(ReadOnlySpan<byte> data, ReadOnlySpan<byte> pattern, ReadOnlySpan<byte> mask, int resultLimit = -1)
    {
        List<int> matches = [];
        if (pattern.Length == 0 || pattern.Length != mask.Length || data.Length < pattern.Length)
        {
            return matches;
        }

        var lastStart = data.Length - pattern.Length;
        for (var start = 0; start <= lastStart; start++)
        {
            if (!IsMatchAt(data, start, pattern, mask))
            {
                continue;
            }

            matches.Add(start);
            if (resultLimit > 0 && matches.Count >= resultLimit)
            {
                break;
            }
        }

        return matches;
    }

    public static bool IsMatchAt(ReadOnlySpan<byte> data, int offset, ReadOnlySpan<byte> pattern, ReadOnlySpan<byte> mask)
    {
        if (offset < 0 || pattern.Length != mask.Length || data.Length - offset < pattern.Length)
        {
            return false;
        }

        for (var i = 0; i < pattern.Length; i++)
        {
            var currentMask = mask[i];
            if (currentMask == 0x00)
            {
                continue;
            }

            if ((data[offset + i] & currentMask) != pattern[i])
            {
                return false;
            }
        }

        return true;
    }
}