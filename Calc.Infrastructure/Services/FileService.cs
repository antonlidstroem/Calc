using System.Text.Json;

namespace Calc.Infrastructure.Services;

public class FileService
{
    public async Task<T?> ReadEncryptedJsonAsync<T>(string filePath, string key)
    {
        if (!File.Exists(filePath)) return default;

        try
        {
            var encryptedData = await File.ReadAllTextAsync(filePath);
            var decryptedJson = EncryptionService.Decrypt(encryptedData, key);
            
            if (string.IsNullOrEmpty(decryptedJson)) return default;
            
            return JsonSerializer.Deserialize<T>(decryptedJson);
        }
        catch
        {
            return default;
        }
    }

    public async Task WriteEncryptedJsonAsync<T>(string filePath, T data, string key)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        var encryptedData = EncryptionService.Encrypt(json, key);
        await File.WriteAllTextAsync(filePath, encryptedData);
    }

    public async Task<string> ReadAssetAsync(string fileName)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}
