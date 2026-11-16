using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Labb03___Quiz_Configurator.Models;

public class QuizPack : INotifyPropertyChanged
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;

    public int TimeLimitSeconds { get; set; } = 30;

    public ObservableCollection<Question> Questions { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;
}
