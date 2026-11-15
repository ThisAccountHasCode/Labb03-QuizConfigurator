namespace Labb03___Quiz_Configurator.Models;

public class Question
{
    public string Text { get; set; } = string.Empty;

    public List<Answer> Answers { get; set; } = [];

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;
}