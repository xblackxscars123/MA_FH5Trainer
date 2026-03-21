namespace XPaint.Resources;

public static class StringCipher
{
    private static readonly byte[] Key = [0xA7, 0x3B, 0xF1, 0x5C, 0x88, 0x2E, 0xD4, 0x69];

    public static string D(string encoded)
    {
        var bytes = Convert.FromBase64String(encoded);
        var result = new byte[bytes.Length];
        for (var i = 0; i < bytes.Length; i++)
        {
            result[i] = (byte)(bytes[i] ^ Key[i % Key.Length]);
        }
        return System.Text.Encoding.UTF8.GetString(result);
    }

    public static string E(string plain)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(plain);
        var result = new byte[bytes.Length];
        for (var i = 0; i < bytes.Length; i++)
        {
            result[i] = (byte)(bytes[i] ^ Key[i % Key.Length]);
        }
        return Convert.ToBase64String(result);
    }
}
