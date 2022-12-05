using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultipleScoreCounter.Models;

public class Card : INotifyPropertyChanged
{
    public Card(string name)
    {
        Name = name;
        Cost = 0; //todo in constructor
        Instant = new List<KeyValuePair<int, int>>(); //todo in constructor
        OnRoundStart = new List<KeyValuePair<int, int>>(); //todo in constructor
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /**
     * Nazev
     */
    public string Name { get; set; }

    /**
     * Cena
     */
    public int Cost;

    /**
     * Prida ihned
     */
    public List<KeyValuePair<int, int>> Instant;

    /**
     * Prida za kolo
     */
    public List<KeyValuePair<int, int>> OnRoundStart;
    
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