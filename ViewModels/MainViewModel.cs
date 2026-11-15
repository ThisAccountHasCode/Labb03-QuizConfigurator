using System.ComponentModel;
using System.Windows.Input;
using Labb03___Quiz_Configurator.Models;

namespace Labb03___Quiz_Configurator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        ChangeTitleCommand = new RelayCommand(() =>
        {
            Title = "New Title";
        });

        CurrentPack = CreateSamplePack();
    }

    public QuizPack CurrentPack { get; }

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

    private Question? _selectedQuestion;
    public Question? SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            _selectedQuestion = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedQuestion)));
        }
    }

    public ICommand ChangeTitleCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private static QuizPack CreateSamplePack()
    {
        QuizPack samplePack = new() { Name = "Geography" };

        Question question = new()
        {
            Text = "What is the capital of France?",
            Difficulty = Difficulty.Easy,
            Answers =
            [
                new Answer { Text = "Paris", IsCorrect = true },
                new Answer { Text = "London", IsCorrect = false },
                new Answer { Text = "Berlin", IsCorrect = false },
                new Answer { Text = "Madrid", IsCorrect = false }
            ]
        };
        samplePack.Questions.Add(question);
        return samplePack;
    }
}
