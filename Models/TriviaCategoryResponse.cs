using System.Text.Json.Serialization;

namespace Labb03___Quiz_Configurator.Models;

public class TriviaCategoryResponse()
{
    [JsonPropertyName("trivia_categories")]
    public List<TriviaCategory> Categories { get; set; } = [];
}
