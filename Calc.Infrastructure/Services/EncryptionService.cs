using System.Security.Cryptography;
using System.Text;

namespace Calc.Infrastructure.Services;

public static class EncryptionService
{
    // Ett fast salt som gör din apps kryptering unik även om andra appar använder samma kod
    private static readonly byte[] InternalSalt = Encoding.UTF8.GetBytes("StealthCalc_System_Salt_2026");

    /// <summary>
    /// Skapar en envägs-hash för att verifiera inmatad kod utan att spara själva koden.
    /// </summary>
    public static string ComputeHash(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input + "System_Security_Pepper");
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Krypterar text med AES-256.
    /// </summary>
    public static string Encrypt(string plainText, string passPhrase)
    {
        using var aes = Aes.Create();
        using var deriveBytes = new Rfc2898DeriveBytes(passPhrase, InternalSalt, 10000, HashAlgorithmName.SHA256);
        
        aes.Key = deriveBytes.GetBytes(32); // AES-256
        aes.IV = deriveBytes.GetBytes(16);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    /// <summary>
    /// Dekrypterar text med AES-256.
    /// </summary>
    public static string Decrypt(string cipherText, string passPhrase)
    {
        try
        {
            using var aes = Aes.Create();
            using var deriveBytes = new Rfc2898DeriveBytes(passPhrase, InternalSalt, 10000, HashAlgorithmName.SHA256);
            
            aes.Key = deriveBytes.GetBytes(32);
            aes.IV = deriveBytes.GetBytes(16);

            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        catch
        {
            return string.Empty;
        }
    }
}
