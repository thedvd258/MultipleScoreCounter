using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;
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
        public ReactiveCommand<Unit, Unit> StartGameCommand { get; }
        
        public List<Player> Players { get; }
        
        /**
         * Slider Label Text property
         */
        public object SliderText
        {
            get => (Floor((double)_sliderValue).ToString(CultureInfo.InvariantCulture));
        }

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
            Players = new List<Player>();
            _sliderValue = 1;
            //NewGameCommand = ReactiveCommand.Create(NewGame);
            ExitCommand = ReactiveCommand.Create(Exit);
            StartGameCommand = ReactiveCommand.Create(StartGame);
        }

        
        /**
         * Start a new game
         */
        private void StartGame()
        {

            //var window = new GameView();
            //window.Show();
            
            int numberOfPlayers = (int)Floor((double)_sliderValue);
            for (int i = 1; i <= numberOfPlayers; ++i)
            {
                Players.Add(new Player(i));
            }

            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var ownerWindow = desktop.MainWindow;
                var window = new GameView
                {
                    DataContext = new GameViewModel(Players)
                };
                window.ShowDialog(ownerWindow);
            }
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