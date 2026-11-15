namespace Labb03___Quiz_Configurator.Models;

public class Question
{
    public string Text { get; set; } = string.Empty;

    public string Answer { get; set; } = string.Empty;

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;
}