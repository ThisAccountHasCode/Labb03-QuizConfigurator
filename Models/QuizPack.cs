namespace Labb03___Quiz_Configurator.Models;

public class QuizPack
{
    public string Name { get; set; } = string.Empty;

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;

    public int TimeLimitSeconds { get; set; } = 30;
}
