using System.Text.Json.Serialization;

namespace Labb03___Quiz_Configurator.Models;

public class TriviaQuestionResponse()
{
    [JsonPropertyName("response_code")] public int ResponseCode { get; set; }
    [JsonPropertyName("results")] public List<TriviaQuestion> Results { get; set; } = [];
}
