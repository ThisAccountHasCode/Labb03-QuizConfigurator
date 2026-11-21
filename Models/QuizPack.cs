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

    private Difficulty _difficulty = Difficulty.Easy;
    public Difficulty Difficulty
    {
        get => _difficulty;
        set
        {
            _difficulty = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Difficulty)));
        }
    }

    private int _timeLimitSeconds = 30;
    public int TimeLimitSeconds
    {
        get => _timeLimitSeconds;
        set
        {
            _timeLimitSeconds = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeLimitSeconds)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasValidTimeLimit)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasTimeLimitElapsed)));
        }
    }

    public bool HasValidTimeLimit => TimeLimitSeconds > 0;

    public bool HasTimeLimitElapsed => TimeLimitSeconds <= 0;

    public ObservableCollection<Question> Questions { get; set; } = [];

    public event PropertyChangedEventHandler? PropertyChanged;
}
