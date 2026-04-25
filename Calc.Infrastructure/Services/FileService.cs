using System.Text.Json;

namespace Calc.Infrastructure.Services;

public class FileService
{
    public async Task<T?> ReadJsonAsync<T>(string filePath)
    {
        if (!File.Exists(filePath)) return default;

        using var stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    public async Task WriteJsonAsync<T>(string filePath, T data)
    {
        using var stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, data, new JsonSerializerOptions { WriteIndented = true });
    }

    public async Task<string> ReadAssetAsync(string fileName)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}