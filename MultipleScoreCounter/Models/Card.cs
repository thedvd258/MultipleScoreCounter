using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultipleScoreCounter.Models;

public class Card : INotifyPropertyChanged
{
    public Card(string name)
    {
        Name = name;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /**
     * Nazev
     */
    public string Name { get; set; }
    
    /**
     * Cena
     */
    
    /**
     * Prida ihned
     */
    
    /**
     * Prida za kolo
     */
    
    
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