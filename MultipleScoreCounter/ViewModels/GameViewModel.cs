using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Threading;
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
        
        public int RoundNumber { get; set; }
        
        
        private bool CanStartRound { get; set; }
        private string ProblemPlayer { get; set; }

        public string RoundText
        {
            get
            {
                if (!CanStartRound)
                {
                    return "Nelze spustit další kolo, hráč " + ProblemPlayer + " by šel do mínusu.";
                }
                return  RoundNumber.ToString() + ". kolo";
            }
        }

        public List<Player> Players { get;}

        private Player? _selectedPlayer;
        public Player? SelectedPlayer
        {
            get => _selectedPlayer;
            set {
                _selectedPlayer = value;
                OnPropertyChanged("_selectedPlayer");
            }
        }

        /**
         * Starts new round in current game
         */
        public void StartNewRound()
        {
            if (!TryStartNewRound())
            {
                OnPropertyChanged("RoundNumber");
                OnPropertyChanged("RoundText");
                return;
            }
            
            foreach (var player in Players)
            {
                player.NewRoundPlayer();
            }

            ++RoundNumber;
            OnPropertyChanged("RoundNumber");
            OnPropertyChanged("RoundText");

        }

        /**
         * Tries to start new round
         */
        private bool TryStartNewRound()
        {
            bool MoneyNegative = false;
            foreach (var player in Players)
            {
                if (player.TryNewRoundPlayer())
                {
                    ProblemPlayer = player.Name;
                    OnPropertyChanged(nameof(ProblemPlayer));
                    MoneyNegative = true;
                }
            }

            CanStartRound = !MoneyNegative;
            OnPropertyChanged(nameof(CanStartRound));
            return CanStartRound;
        }


        public void removePlayer(Player player)
        {
            Players.Remove(player);
        }
        
        

        public void Refresh()
        {
            OnPropertyChanged("Players");
        }

        public GameViewModel(IEnumerable<Player> items, IEnumerable<Card> cards)
        {
            Cards = new ObservableCollection<Card>(cards);
            ExitCommand = ReactiveCommand.Create(Exit);
            Players = new List<Player>(items);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Players)));
            RoundNumber = 1;
            CanStartRound = true;
            ProblemPlayer = "error";
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