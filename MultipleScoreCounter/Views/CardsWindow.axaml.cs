using System;

using Avalonia.ReactiveUI;
using MultipleScoreCounter.ViewModels;
using ReactiveUI;

namespace MultipleScoreCounter.Views;
public partial class CardsWindow : ReactiveWindow<CardsViewModel>
{
    public CardsWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.SelectCardCommand.Subscribe(Close)));
    }
}