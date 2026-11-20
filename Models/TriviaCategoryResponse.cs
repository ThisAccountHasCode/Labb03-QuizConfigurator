using System.Text.Json.Serialization;

namespace Labb03___Quiz_Configurator.Models;

public class TriviaCategoryResponse()
{
    [JsonPropertyName("trivia_categories")]
    public List<TriviaCategory> Categories { get; set; } = [];
    [JsonPropertyName("response_code")] public int ResponseCode { get; set; } = 0;
    [JsonPropertyName("results")] public List<TriviaQuestion> Results { get; set; } = [];
}
