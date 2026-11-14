using System.ComponentModel;
using System.Windows.Input;

namespace Labb03___Quiz_Configurator;

public class RelayCommand : ICommand
{
    public RelayCommand(Action execute)
    {
        _execute = execute;
    }

    private readonly Action _execute;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _execute();
    }
}
