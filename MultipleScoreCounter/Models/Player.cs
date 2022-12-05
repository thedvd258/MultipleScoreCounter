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
    //---------------------------------
    /**
     * Column 0
     */
    public int money { get; set; }
    
    /**
     * Column 1
     */
    public int onePercent { get; set; }
    
    /**
     * Column 2
     */
    public int LGBT { get; set; }
    
    /**
     * Column 3
     */
    public int etnics { get; set; }
    
    /**
     * Column 4
     */
    public int smallBussiness { get; set; }
    
    /**
     * Column 5
     */
    public int students { get; set; }
    
    /**
     * Column 6
     */
    public int elderly { get; set; }
    
    /**
     * Column 7
     */
    public int proletariat { get; set; }
    
    /**
     * Column 8
     */
    public int families { get; set; }
    
    /**
     * Column 9
     */
    public int samozivitele { get; set; }
    
    /**
     * Column 10
     */
    public int unemployed { get; set; }
    
    /**
     * Column 11
     */
    public int inteligence { get; set; }
    
    /**
     * Column 12
     */
    public int agrary { get; set; }
    
    /**
     * Column 13
     */
    public int religious { get; set; }
    
    /**
     * Column 14
     */
    public int patriots { get; set; }
    
    /**
     * Column 15
     */
    public int soldiers { get; set; }
    
    /**
     * Column 16
     */
    public int emigrants { get; set; }
    
    /**
     * Column 17
     */
    public int officers { get; set; }
    
    //----------------------------------
    
    
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
        money = 0;
        onePercent = LGBT = etnics = smallBussiness = students = elderly = proletariat = families = samozivitele = unemployed = inteligence = agrary = religious = patriots = soldiers = officers = emigrants = 0;
        
        Cards = new List<Card>();
        Cards.Add(new Card("Card 1"));
    }

    private void AddOne(string column)
    {
        var columnNum = int.Parse(column);
        switch (columnNum)
        {
            case 0:
                ++onePercent;
                OnPropertyChanged("onePercent");
                break;
            case 1:
                ++LGBT;
                OnPropertyChanged("LGBT");
                break;
            case 2:
                ++etnics;
                OnPropertyChanged("etnics");
                break;
        }
    }

    private void RemoveOne(string column)
    {
        var columnNum = int.Parse(column);
        switch (columnNum)
        {
            case 0:
                --onePercent;
                OnPropertyChanged("onePercent");
                break;
            case 1:
                --LGBT;
                OnPropertyChanged("LGBT");
                break;
            case 2:
                --etnics;
                OnPropertyChanged("etnics");
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