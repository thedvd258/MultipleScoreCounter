using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MultipleScoreCounter.Models;
using MultipleScoreCounter.Views;
using ReactiveUI;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        
        
        public ObservableCollection<Card> Cards { get; }
        
        public List<Player> Players { get;}

        private Player? _selectedPlayer;
        private int? _selectedPlayerNum;
        public Player? SelectedPlayer
        {
            get => _selectedPlayer;
            set {
                _selectedPlayer = value;
                _selectedPlayerNum = value?.Number;
                OnPropertyChanged("_selectedPlayer");
                OnPropertyChanged("_selectedPlayerNum");
            }
        }

        /**
         * Starts new round in current game
         */
        public void StartNewRound()
        {
            foreach (var player in Players)
            {
                player.NewRoundPlayer();
            }
        }

        public GameViewModel(IEnumerable<Player> items, IEnumerable<Card> cards)
        {
            Cards = new ObservableCollection<Card>(cards);
            ExitCommand = ReactiveCommand.Create(Exit);
            Players = new List<Player>(items);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Players)));
        }

        

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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