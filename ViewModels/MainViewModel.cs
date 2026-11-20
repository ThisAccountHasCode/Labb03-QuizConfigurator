using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Labb03___Quiz_Configurator.Models;
using Labb03___Quiz_Configurator.Services;

namespace Labb03___Quiz_Configurator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public MainViewModel()
    {
        AddQuestionCommand = new RelayCommand(AddQuestion);
        AddPackCommand = new RelayCommand(AddPack);
        DeletePackCommand = new RelayCommand(DeletePack);
        SetCorrectAnswerCommand = new RelayCommand(SetCorrectAnswer);
        TogglePackOptionsCommand = new RelayCommand(TogglePackOptions);
        SavePackCommand = new AsyncRelayCommand(SavePackAsync);
        OpenPackCommand = new AsyncRelayCommand(OpenPackAsync);
        StartGameCommand = new RelayCommand(StartGame);
        LoadTriviaCategoriesCommand = new AsyncRelayCommand(LoadTriviaCategoriesAsync);
        DeleteQuestionCommand = new RelayCommand(DeleteQuestion,
        () => SelectedPack != null && SelectedQuestion != null);

        CurrentPack = CreateSamplePack();

        QuizPacks.Add(CurrentPack);

        SelectedPack = CurrentPack;
    }

    // Commands
    public ICommand AddQuestionCommand { get; }
    public ICommand AddPackCommand { get; }
    public ICommand DeletePackCommand { get; }
    public ICommand SetCorrectAnswerCommand { get; }
    public ICommand TogglePackOptionsCommand { get; }
    public ICommand SavePackCommand { get; }
    public ICommand OpenPackCommand { get; }
    public ICommand StartGameCommand { get; }
    public RelayCommand DeleteQuestionCommand { get; }
    public AsyncRelayCommand LoadTriviaCategoriesCommand { get; }

    // Events
    public event PropertyChangedEventHandler? PropertyChanged;
    public event Action<QuizPack>? PlayRequested;

    // Properties
    public QuizPack CurrentPack { get; }
    public ObservableCollection<QuizPack> QuizPacks { get; } = [];
    public ObservableCollection<TriviaCategory> TriviaCategories { get; } = [];
    public Difficulty[] Difficulties { get; } = Enum.GetValues<Difficulty>();

    private QuizPack? _selectedPack;
    public QuizPack? SelectedPack
    {
        get => _selectedPack;
        set
        {
            _selectedPack = value;
            DeleteQuestionCommand.RaiseCanExecuteChanged();
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
            DeleteQuestionCommand.RaiseCanExecuteChanged();
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

    private bool _isPackOptionsOpen;
    public bool IsPackOptionsOpen
    {
        get => _isPackOptionsOpen;
        set
        {
            _isPackOptionsOpen = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPackOptionsOpen)));
        }
    }

    private readonly PackStorage _packStorage = new();
    private readonly TriviaClientApi _triviaClient = new();

    // Methods
    private void AddQuestion()
    {
        if (SelectedPack == null) { return; }

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
        if (SelectedPack == null || SelectedQuestion == null) { return; }
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
        if (SelectedQuestion == null || SelectedAnswer == null) { return; }

        if (!SelectedQuestion.Answers.Contains(SelectedAnswer)) { return; }

        foreach (var answer in SelectedQuestion.Answers)
        {
            answer.IsCorrect = answer == SelectedAnswer;
        }
    }

    private void TogglePackOptions()
    {
        IsPackOptionsOpen = !IsPackOptionsOpen;
    }

    private async Task SavePackAsync()
    {
        if (SelectedPack == null) { return; }

        SaveFileDialog dialog = new();

        dialog.Filter = "JSON files (*.json)|*.json";

        if (dialog.ShowDialog() != true) { return; }

        try
        {
            await _packStorage.SaveAsync(SelectedPack, dialog.FileName);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not save the pack: {ex.Message}");
        }
    }

    private async Task OpenPackAsync()
    {
        OpenFileDialog dialog = new();
        dialog.Filter = "JSON files (*.json)|*.json";

        if (dialog.ShowDialog() != true) { return; }

        try
        {
            QuizPack loadedPack = await _packStorage.LoadAsync(dialog.FileName);
            QuizPacks.Add(loadedPack);
            SelectedPack = loadedPack;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not open the pack: {ex.Message}");
        }
    }

    private async Task LoadTriviaCategoriesAsync()
    {
        try
        {
            List<TriviaCategory> categories = await _triviaClient.GetCategoriesAsync();

            TriviaCategories.Clear();

            foreach (TriviaCategory category in categories)
            {
                TriviaCategories.Add(category);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not load trivia categories: {ex.Message}");
        }
    }

    private void StartGame()
    {
        if (SelectedPack == null) { return; }

        if (SelectedPack.Questions.Count == 0 || !SelectedPack.HasValidTimeLimit || SelectedPack.Questions.Any(question => !question.IsValid))
        {
            MessageBox.Show("Add at least one valid question and set a positive time limit before playing.");
            return;
        }

        PlayRequested?.Invoke(SelectedPack);
    }

    private static QuizPack CreateSamplePack()  // This method creates a sample QuizPack and its answers - It will be deleted
    {
        QuizPack samplePack = new()
        {
            Name = "Geography",
            Questions =
            [
                new Question
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
                },
                new Question{
                    Text = "In what country is the capital city named Stockholm?",
                    Difficulty  = Difficulty.Easy,
                    Answers =
                    [
                        new Answer { Text = "Sweden", IsCorrect = true },
                        new Answer { Text = "Norway", IsCorrect = false },
                        new Answer { Text = "Finland", IsCorrect = false },
                        new Answer { Text = "Estonia", IsCorrect = false }
                    ]
                }
            ]
        };

        return samplePack;
    }
}
