using System.Net.Http;
using System.Text.Json;
using Labb03___Quiz_Configurator.Models;

namespace Labb03___Quiz_Configurator.Services;

public class TriviaClientApi
{
    private static readonly HttpClient _httpClient = new();

    public async Task<List<TriviaCategory>> GetCategoriesAsync()
    {
        string json = await _httpClient.GetStringAsync("https://opentdb.com/api_category.php");
        TriviaCategoryResponse response = JsonSerializer.Deserialize<TriviaCategoryResponse>(json)
        ?? throw new JsonException("The category response was empty.");
        return response.Categories;
    }

    public async Task<TriviaQuestionResponse> GetQuestionsAsync(int categoryId, int amount, Difficulty difficulty)
    {
        string url = $"https://opentdb.com/api.php?amount={amount}&category={categoryId}&type=multiple&difficulty={difficulty.ToString().ToLowerInvariant()}";
        string json = await _httpClient.GetStringAsync(url);
        TriviaQuestionResponse response = JsonSerializer.Deserialize<TriviaQuestionResponse>(json)
        ?? throw new JsonException("The question response was empty.");
        if (response.ResponseCode != 0)
        {
            string message = response.ResponseCode switch
            {
                1 => "Not enough questions match the selected settings.",
                5 => "Too many requests. Wait a few seconds and try again.",
                _ => $"The trivia API returned error code {response.ResponseCode}."
            };

            throw new InvalidOperationException(message);
        }
        return response;
    }
}
