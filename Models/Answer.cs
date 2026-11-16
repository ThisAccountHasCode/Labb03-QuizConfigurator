using System.ComponentModel;

namespace Labb03___Quiz_Configurator.Models;

public class Answer : INotifyPropertyChanged
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

    public bool IsCorrect { get; set; } = false;

    public event PropertyChangedEventHandler? PropertyChanged;
}
