using System.ComponentModel;
using System.Windows.Input;

namespace Labb03___Quiz_Configurator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        ChangeTitleCommand = new RelayCommand(() =>
        {
            Title = "New Title";
        });
    }

    public ICommand ChangeTitleCommand { get; }

    private string _title = "Quiz Configurator";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

}
