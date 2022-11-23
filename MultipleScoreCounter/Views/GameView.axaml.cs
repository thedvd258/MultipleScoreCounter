using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using MultipleScoreCounter.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace MultipleScoreCounter.Views;

public partial class GameView :  ReactiveWindow<GameViewModel>
{
    public GameView()
    {
        InitializeComponent();
        this.AttachDevTools();
        this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    /**
    * Show Async Cards library
    */
    private async Task DoShowDialogAsync(InteractionContext<CardsViewModel, CardViewModel?> interaction)
    {
        var dialog = new CardsWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<CardViewModel?>(this);
        interaction.SetOutput(result);
    }
}