
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

namespace MultipleScoreCounter.Models;

public sealed class Card : INotifyPropertyChanged
{
    public Card(string name, int cost, List<Tuple<int,int>> instant, List<Tuple<int,int>> roundStart)
    {
        Name = name;
        Cost = cost;
        
        Instant = new List<Tuple<int,int>>();
        foreach (var pair in instant)
        {
            Instant.Add(pair);
        }
         
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
     * Prida ihned
     */
    public List<Tuple<int, int>> Instant;

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