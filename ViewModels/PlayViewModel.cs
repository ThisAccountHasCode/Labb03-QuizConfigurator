using System.Windows.Threading;
using System.Windows.Input;
using System.ComponentModel;
using Labb03___Quiz_Configurator.Models;

namespace Labb03___Quiz_Configurator.ViewModels;

public class PlayViewModel : INotifyPropertyChanged
{
    public PlayViewModel(QuizPack pack)
    {
        SubmitAnswerCommand = new RelayCommand(SubmitAnswer);
        NextQuestionCommand = new RelayCommand(NextQuestion);

        Pack = pack;
        RemainingTimeSeconds = pack.TimeLimitSeconds;
        Questions = pack.Questions.ToArray();
        Random.Shared.Shuffle(Questions);
        PrepareCurrentAnswersList();

        _questionTimer.Interval = TimeSpan.FromSeconds(1);
        _questionTimer.Tick += OnQuestionTimerTick;
        _questionTimer.Start();
    }

    // Commands
    public ICommand SubmitAnswerCommand { get; }
    public ICommand NextQuestionCommand { get; }

    // Events
    public event PropertyChangedEventHandler? PropertyChanged;

    // Fields
    private readonly DispatcherTimer _questionTimer = new();

    // Properties
    public QuizPack Pack { get; }
    public Question[] Questions { get; }
    private int _questionIndex = 0;

    public Question? CurrentQuestion =>
    _questionIndex < Questions.Length ? Questions[_questionIndex] : null;

    public string QuestionProgress =>
    CurrentQuestion == null ? string.Empty : $"Question {_questionIndex + 1} of {Questions.Length}";

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

    private int? _remainingSeconds;
    public int RemainingTimeSeconds
    {
        get => _remainingSeconds ?? 0;
        set
        {
            _remainingSeconds = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RemainingTimeSeconds)));
        }
    }

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

        _questionTimer.Stop();

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

    private void NextQuestion()
    {
        if (!HasAnswered || CurrentQuestion == null) { return; }

        _questionIndex++;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionProgress)));

        // Check if quiz is completed
        if (_questionIndex >= Questions.Length)
        {
            CurrentAnswers = [];
            Feedback = $"Quiz complete! Score: {Score} / {Questions.Length}";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuestion)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentAnswers)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Feedback)));
            return;
        }

        // Next question
        SelectedAnswer = null;
        HasAnswered = false;
        Feedback = string.Empty;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuestion)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasAnswered)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Feedback)));

        PrepareCurrentAnswersList();
        RemainingTimeSeconds = Pack.TimeLimitSeconds;
        _questionTimer.Start();
    }

    private void OnQuestionTimerTick(object? sender, EventArgs e)
    {
        if (RemainingTimeSeconds > 0)
        {
            RemainingTimeSeconds--;
            if (RemainingTimeSeconds == 0)
            {
                _questionTimer.Stop();
                HasAnswered = true;

                Answer correctAnswer = CurrentAnswers.First(answer => answer.IsCorrect);
                Feedback = $"Time is up! The correct answer was: {correctAnswer.Text}.";

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasAnswered)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Feedback)));
            }
        }
    }

    public void StopTimer()
    {
        _questionTimer.Stop();
    }
}
