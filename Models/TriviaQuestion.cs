using System.Text.Json.Serialization;

namespace Labb03___Quiz_Configurator.Models;

public class TriviaQuestion()
{
    // Properties
    [JsonPropertyName("question")] public string Question { get; set; } = string.Empty;
    [JsonPropertyName("correct_answer")] public string CorrectAnswer { get; set; } = string.Empty;
    [JsonPropertyName("incorrect_answers")] public List<string> IncorrectAnswers { get; set; } = [];
}
