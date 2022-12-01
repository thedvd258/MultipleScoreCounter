using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace MultipleScoreCounter.Models;

public class Player : INotifyPropertyChanged
{
    public int Number { get; set; }

    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged("Name");
        }
    }
    
    public int Banana { get; set; }
    public int Apple { get; set; }
    public int Cucumber { get; set; }
    
    /**
     * Used to buy cards
     */
    public int Coins { get; set; }
    
    /**
     * Players active cards
     */
    public List<Card> Cards { get; set; }
    
    public ReactiveCommand<string, Unit> AddOneCommand { get; }
    public ReactiveCommand<string, Unit> RemoveOneCommand { get; }

    public Player(int number)
    {
        _name = "Hráč " + number;
        Number = number;
        AddOneCommand = ReactiveCommand.Create<string>(AddOne);
        RemoveOneCommand = ReactiveCommand.Create<string>(RemoveOne);
        Banana = Apple = Cucumber = 0;
        
        Coins = 0;
        Cards = new List<Card>();
        Cards.Add(new Card("Card 1"));
    }

    private void AddOne(string column)
    {
        var columnNum = int.Parse(column);
        switch (columnNum)
        {
            case 0:
                ++Banana;
                OnPropertyChanged("Banana");
                break;
            case 1:
                ++Apple;
                OnPropertyChanged("Apple");
                break;
            case 2:
                ++Cucumber;
                OnPropertyChanged("Cucumber");
                break;
        }
    }

    private void RemoveOne(string column)
    {
        var columnNum = int.Parse(column);
        switch (columnNum)
        {
            case 0:
                if (Banana < 1)
                    return;
                --Banana;
                OnPropertyChanged("Banana");
                break;
            case 1:
                if (Apple < 1)
                    return;
                --Apple;
                OnPropertyChanged("Apple");
                break;
            case 2:
                if (Cucumber < 1)
                    return;
                --Cucumber;
                OnPropertyChanged("Cucumber");
                break;
        }
        
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}