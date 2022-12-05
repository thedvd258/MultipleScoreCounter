using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using MultipleScoreCounter.Models;

namespace MultipleScoreCounter.Views;

public partial class GameView : Window
{
    public GameView()
    {
        InitializeComponent();
        this.AttachDevTools();
    }
}