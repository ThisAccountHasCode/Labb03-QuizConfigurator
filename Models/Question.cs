using System.ComponentModel;

namespace Labb03___Quiz_Configurator.Models;

public class Question : INotifyPropertyChanged
{
    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }
    }

    public List<Answer> Answers { get; set; } = [];

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;

    public event PropertyChangedEventHandler? PropertyChanged;
}
