using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;
using ReactiveUI;
using static System.Math;

namespace MultipleScoreCounter.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        //public ReactiveCommand<Unit, Unit> NewGameCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public object SliderText
        {
            get => (Floor((double)_sliderValue).ToString(CultureInfo.InvariantCulture));
        }

        private object _sliderValue;
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

        public MainWindowViewModel()
        {
            this._sliderValue = 1;
            //NewGameCommand = ReactiveCommand.Create(NewGame);
            ExitCommand = ReactiveCommand.Create(Exit);
            
        }

        //private void NewGame()
        //{
        //    
        //}

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