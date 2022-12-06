
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

namespace MultipleScoreCounter.Models;

public sealed class Card : INotifyPropertyChanged
{
    public Card(string name, int cost, int costBurn, List<Tuple<int,int>> roundStart)
    {
        Name = name;
        Cost = cost;
        CostBurn = costBurn;

        OnRoundStart = new List<Tuple<int,int>>();
        foreach (var pair in roundStart)
        {
            OnRoundStart.Add(pair);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /**
     * Nazev
     */
    public string Name { get; set; }

    /**
     * Cena
     */
    public int Cost { get; set; }

    /**
     * Cena spaleni
     */
    public int CostBurn { get; set; }
    
    public string CostText {
        get
        {
            return "Cena zahrát: " + Cost.ToString();
        }
    }
    
    public string BurnText {
        get
        {
            return "Cena zrušit: " + CostBurn.ToString();
        }
    }

    /**
     * Prida za kolo
     */
    public List<Tuple<int, int>> OnRoundStart;

    public void PlayThisCard(Player? player)
    {
        player?.PlayCard(this);
        Refresh();
        //OnPropertyChanged("Card");
        //Dispatcher.UIThread.InvokeAsync(() => OnPropertyChanged(nameof(Card)));
    }
    
    public void RemoveThisCard(Player? player)
    {
        player?.RemoveCard(this);
        Refresh();
        //OnPropertyChanged("Card");
        //Dispatcher.UIThread.InvokeAsync(() => OnPropertyChanged(nameof(Card)));
    }

    public void Refresh()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Card"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
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
}