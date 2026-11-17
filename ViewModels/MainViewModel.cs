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
        SetCorrectAnswerCommand = new RelayCommand(SetCorrectAnswer);
        DeleteQuestionCommand = new RelayCommand(DeleteQuestion);

        CurrentPack = CreateSamplePack();

        QuizPacks.Add(CurrentPack);

        SelectedPack = CurrentPack;
    }

    // Commands
    public ICommand AddQuestionCommand { get; }
    public ICommand AddPackCommand { get; }
    public ICommand DeletePackCommand { get; }
    public ICommand SetCorrectAnswerCommand { get; }
    public ICommand DeleteQuestionCommand { get; }

    // Events
    public event PropertyChangedEventHandler? PropertyChanged;

    // Properties
    public QuizPack CurrentPack { get; }
    public ObservableCollection<QuizPack> QuizPacks { get; } = [];
    public Difficulty[] Difficulties { get; } = Enum.GetValues<Difficulty>();

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

    private Answer? _selectedAnswer;
    public Answer? SelectedAnswer
    {
        get => _selectedAnswer;
        set
        {
            _selectedAnswer = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAnswer)));
        }
    }

    private int _remainingSeconds;
    public int RemainingSeconds
    {
        get => _remainingSeconds;
        set
        {
            _remainingSeconds = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RemainingSeconds)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTimeRunningLow)));
        }
    }
    public bool IsTimeRunningLow => RemainingSeconds < 10;

    // Methods

    private void AddQuestion()
    {
        if (SelectedPack == null)
        {
            return;
        }
        Question newQuestion = new()
        {
            Text = "New Question",
            Answers =
            [
                new Answer(),
                new Answer(),
                new Answer(),
                new Answer(),
            ]
        };
        SelectedPack.Questions.Add(newQuestion);
        SelectedQuestion = newQuestion;
    }

    private void DeleteQuestion()
    {
        if (SelectedPack == null || SelectedQuestion == null)
        {
            return;
        }
        SelectedPack.Questions.Remove(SelectedQuestion);
        SelectedQuestion = SelectedPack.Questions.FirstOrDefault();
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

    private void SetCorrectAnswer()
    {
        if (SelectedQuestion == null || SelectedAnswer == null)
        {
            return;
        }

        if (!SelectedQuestion.Answers.Contains(SelectedAnswer))
        {
            return;
        }

        foreach (var answer in SelectedQuestion.Answers)
        {
            answer.IsCorrect = answer == SelectedAnswer;
        }
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
