using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace MultipleScoreCounter.Views;

public partial class GameView : Window
{
    public GameView()
    {
        InitializeComponent();
        this.AttachDevTools();
    }
}