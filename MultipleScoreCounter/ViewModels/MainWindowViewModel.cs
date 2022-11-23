using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MultipleScoreCounter.Models;
using MultipleScoreCounter.Views;
using ReactiveUI;
using static System.Math;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        //public ReactiveCommand<Unit, Unit> NewGameCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> StartPreviousGameCommand { get; }
        public ReactiveCommand<Unit, Unit> StartNewGameCommand { get; }
        public object FirstGame => _firstGame;
        private bool _firstGame;
        private List<Player> Players { get; }
        private List<Card> cardDatabase { get; }
        
        /**
         * Slider Label Text property
         */
        public object SliderText => (Floor((double)_sliderValue).ToString(CultureInfo.InvariantCulture));

        /**
         * Slider value
         */
        private object _sliderValue;
        
        /**
         * Slider value property
         */
        public object SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderText)));
            }
        }

        /**
         * Main Window Constructor
         */
        public MainWindowViewModel()
        {
            this.cardDatabase = new List<Card>();
            Players = new List<Player>();
            _sliderValue = 1;
            _firstGame = true;
            ExitCommand = ReactiveCommand.Create(Exit);
            StartNewGameCommand = ReactiveCommand.Create(StartNewGame);
            StartPreviousGameCommand = ReactiveCommand.Create(StartPreviousGame);
        }

        private void fillCardDatabase()
        {
            cardDatabase.Add(new Card("Pico"));
            cardDatabase.Add(new Card("Kunda"));
            cardDatabase.Add(new Card("Zmrd"));
        }
        
        /**
         * Starts a new game
         */
        private void StartNewGame()
        {
            Players.Clear();
            var numberOfPlayers = (int)Floor((double)_sliderValue);
            for (var i = 1; i <= numberOfPlayers; ++i)
            {
                Players.Add(new Player(i));
            }

            if (cardDatabase.Count <= 0)
            {
                fillCardDatabase();
            }

            _firstGame = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstGame)));

            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var ownerWindow = desktop.MainWindow;
            var window = new GameView
            {
                DataContext = new GameViewModel(Players, cardDatabase)
            };
            window.ShowDialog(ownerWindow);
        }
        
        


        /**
         * Start a previous game
         */
        private void StartPreviousGame()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
            var ownerWindow = desktop.MainWindow;
            var window = new GameView
            {
                DataContext = new GameViewModel(Players, cardDatabase)
            };
            window.ShowDialog(ownerWindow);
        }

        /**
         * Exits the Application
         */
        private void Exit()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                desktopLifetime.Shutdown();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
    }
}