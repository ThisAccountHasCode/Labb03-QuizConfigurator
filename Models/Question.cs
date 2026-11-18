using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Labb03___Quiz_Configurator.Models;

public class Question : INotifyPropertyChanged
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasQuestionText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }
    }

    private List<Answer> _answers = [];
    public List<Answer> Answers
    {
        get => _answers;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value.Any(answer => answer is null))
            {
                throw new ArgumentException("The answer list contains an empty answer field.", nameof(value));
            }

            if (_answers != value)
            {
                foreach (Answer answer in _answers)
                {
                    answer.PropertyChanged -= OnAnswerPropertyChanged; // unsubscribes it from the old answers when the list is replaced
                }

                _answers = value;

                foreach (Answer answer in _answers)
                {
                    answer.PropertyChanged += OnAnswerPropertyChanged; // subscribes the question’s handler to each answer’s event
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answers)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasDuplicateAnswers)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasOneCorrectAnswer)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasFourAnswers)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }
    }

    public bool HasQuestionText => !string.IsNullOrWhiteSpace(Text);

    public Difficulty Difficulty { get; set; } = Difficulty.Easy;

    public bool HasDuplicateAnswers => Answers
      .Where(answer => answer.HasAnswerText)
      .GroupBy(answer => answer.Text.Trim(), StringComparer.OrdinalIgnoreCase)
      .Any(group => group.Count() > 1);

    public bool HasOneCorrectAnswer => Answers.Count(answer => answer.IsCorrect) == 1;
    public bool HasFourAnswers =>
                Answers.Count == 4 && Answers.All(answer => answer.HasAnswerText);

    public bool IsValid => HasQuestionText && HasFourAnswers && HasOneCorrectAnswer && !HasDuplicateAnswers;

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnAnswerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Answer.Text))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasDuplicateAnswers)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasFourAnswers)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
        }

        if (e.PropertyName == nameof(Answer.IsCorrect))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasOneCorrectAnswer)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
        }
    }
}
