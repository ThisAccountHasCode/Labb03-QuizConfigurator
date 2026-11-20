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
}
