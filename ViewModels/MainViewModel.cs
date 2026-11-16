using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Labb03___Quiz_Configurator.Models;

namespace Labb03___Quiz_Configurator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        AddQuestionCommand = new RelayCommand(AddQuestion);

        AddPackCommand = new RelayCommand(AddPack);

        DeletePackCommand = new RelayCommand(DeletePack);

        CurrentPack = CreateSamplePack();

        QuizPacks.Add(CurrentPack);

        SelectedPack = CurrentPack;
    }

    public QuizPack CurrentPack { get; }

    public ObservableCollection<QuizPack> QuizPacks { get; } = [];

    private QuizPack? _selectedPack;
    public QuizPack? SelectedPack
    {
        get => _selectedPack;
        set
        {
            _selectedPack = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPack)));
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

    public ICommand AddQuestionCommand { get; }

    public ICommand AddPackCommand { get; }

    public ICommand DeletePackCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void AddQuestion()
    {
        if (SelectedPack == null)
        {
            return;
        }
        Question newQuestion = new()
        {
            Text = "New Question"
        };
        SelectedPack.Questions.Add(newQuestion);
    }

    private void AddPack()
    {
        QuizPack newPack = new()
        {
            Name = "New Pack"
        };
        QuizPacks.Add(newPack);
        SelectedPack = newPack;
    }

    private void DeletePack()
    {
        if (SelectedPack == null)
        {
            return;
        }
        QuizPacks.Remove(SelectedPack);
        SelectedPack = QuizPacks.FirstOrDefault();
    }

    private static QuizPack CreateSamplePack()  // This method creates a sample QuizPack with one question and its answers - It will be deleted
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
