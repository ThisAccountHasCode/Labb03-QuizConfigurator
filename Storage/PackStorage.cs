using Labb03___Quiz_Configurator.Models;
using System.IO;
using System.Text.Json;

namespace Labb03___Quiz_Configurator.Storage;

public class PackStorage
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        IgnoreReadOnlyProperties = true
    };

    public async Task SaveAsync(QuizPack pack, string filePath)
    {
        string temporaryPath = Path.GetFullPath(filePath) + "." + Guid.NewGuid().ToString("N") + ".tmp";
        bool temporaryFileCreated = false;

        try
        {
            await using (FileStream stream = new FileStream(temporaryPath, FileMode.CreateNew))
            {
                temporaryFileCreated = true;
                await JsonSerializer.SerializeAsync(stream, pack, _jsonOptions);
            }

            File.Move(temporaryPath, filePath, overwrite: true);
        }
        finally
        {
            if (temporaryFileCreated && File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }

    public async Task<QuizPack> LoadAsync(string filePath)
    {
        await using FileStream stream = File.OpenRead(filePath);
        QuizPack? pack = await JsonSerializer.DeserializeAsync<QuizPack>(stream) ?? throw new JsonException("The file does not contain valid quiz pack data.");
        return pack;
    }
}
