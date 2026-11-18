using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Labb03___Quiz_Configurator.Models;
using Labb03___Quiz_Configurator.Storage;

namespace Labb03___Quiz_Configurator.ViewModels;

public class PlayViewModel : INotifyPropertyChanged
{
    public PlayViewModel(QuizPack pack)
    {
        SubmitAnswerCommand = new RelayCommand(SubmitAnswer);
        Pack = pack;
        Questions = pack.Questions.ToArray();
        Random.Shared.Shuffle(Questions);
        PrepareCurrentAnswersList();
    }

    // Commands
    public ICommand SubmitAnswerCommand { get; }

    // Events
    public event PropertyChangedEventHandler? PropertyChanged;

    // Properties
    public QuizPack Pack { get; }
    public Question[] Questions { get; }
    private int _questionIndex = 0;

    public Question? CurrentQuestion =>
    _questionIndex < Questions.Length ? Questions[_questionIndex] : null;

    public Answer[] CurrentAnswers { get; private set; } = [];

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

    public int Score { get; private set; }

    public bool HasAnswered { get; private set; }

    public string Feedback { get; private set; } = string.Empty;

    // Methods
    private void PrepareCurrentAnswersList()
    {
        if (CurrentQuestion == null) { return; }

        CurrentAnswers = CurrentQuestion.Answers.ToArray();

        Random.Shared.Shuffle(CurrentAnswers);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAnswers)));
    }

    private void SubmitAnswer()
    {
        if (CurrentQuestion == null || SelectedAnswer == null || HasAnswered) { return; }

        HasAnswered = true;

        if (SelectedAnswer.IsCorrect == true)
        {
            Score++;
        }

        Answer correctAnswer = CurrentAnswers.First(answer => answer.IsCorrect);
        Feedback = SelectedAnswer.IsCorrect ? $"Correct! The answer is {correctAnswer.Text}." : $"Incorrect. The correct answer is {correctAnswer.Text}.";

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasAnswered)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Feedback)));
    }
}
