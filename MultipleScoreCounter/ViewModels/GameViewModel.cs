using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MultipleScoreCounter.Models;
using MultipleScoreCounter.Views;
using ReactiveUI;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class GameViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChangedEvent;
        
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> CardsLibraryCommand { get; }
        
        public List<Player> Players { get;}
        public Interaction<CardsViewModel, CardViewModel?> ShowDialog { get; }
        
        public ICommand SelectCardCommand { get; }
        
        /**
         * Constructor
         */
        public GameViewModel(IEnumerable<Player> items)
        {
            
            ShowDialog = new Interaction<CardsViewModel, CardViewModel?>();
            SelectCardCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new CardsViewModel();

                var result = await ShowDialog.Handle(store);
            });
            
            ExitCommand = ReactiveCommand.Create(Exit);
            CardsLibraryCommand = ReactiveCommand.Create(OpenCardsLibrary);
            Players = new List<Player>(items);
            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(Players)));
        }

        private void OpenCardsLibrary()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var ownerWindow = desktop.MainWindow;
            var window = new CardsWindow()
            {
                //DataContext = new CardViewModel()
            };
            var result = window.ShowDialog<CardView>(desktop.MainWindow.OwnedWindows[0]);

            //result.WaitAsync(System.Threading.CancellationToken.None);
            
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        /**
         * Exits the Application
         */
        private void Exit()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                desktopLifetime.MainWindow.OwnedWindows[0].Close();
            }
        }

    }
}