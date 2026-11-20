using System.Text.Json.Serialization;

namespace Labb03___Quiz_Configurator.Models;

public class TriviaCategory()
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}
