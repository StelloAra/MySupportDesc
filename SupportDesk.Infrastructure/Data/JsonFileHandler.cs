using System.Text.Json;

namespace SupportDesk.Infrastructure.Data;

public class JsonFileHandler
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public async Task<List<T>> ReadAsync<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        try
        {
            var json = await File.ReadAllTextAsync(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new InvalidDataException(
                    $"Data file is empty: {filePath}");
            }

            var data = JsonSerializer.Deserialize<List<T>>(
                json,
                _options);

            if (data == null)
            {
                throw new InvalidDataException(
                    $"Data file contains invalid data: {filePath}");
            }

            return data;
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException(
                $"Could not read data file: {filePath}",
                ex);
        }
        catch (IOException ex)
        {
            throw new IOException(
                $"Could not access data file: {filePath}",
                ex);
        }
    }

    public async Task WriteAsync<T>(
        string filePath,
        List<T> data)
    {
        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(data, _options);

        var temporaryFilePath = filePath + ".tmp";

        try
        {
            await File.WriteAllTextAsync(
                temporaryFilePath,
                json);

            File.Move(
                temporaryFilePath,
                filePath,
                true);
        }
        catch
        {
            if (File.Exists(temporaryFilePath))
            {
                File.Delete(temporaryFilePath);
            }

            throw;
        }
    }
}