using System.Windows;
using Labb03___Quiz_Configurator.Models;
using Labb03___Quiz_Configurator.ViewModels;

namespace Labb03___Quiz_Configurator;

public partial class PlayWindow : Window
{
    public PlayWindow(QuizPack pack)
    {
        InitializeComponent();
        PlayViewModel viewModel = new(pack);
        DataContext = viewModel;
        Closed += (_, _) => viewModel.StopTimer();
    }
}
