using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
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
    public List<Card> Cards
    {
        get;
        set;
    }

    public ObservableCollection<Card> CardsCollection { get; set; }

    public ReactiveCommand<string, Unit> AddOneCommand { get; }
    public ReactiveCommand<string, Unit> RemoveOneCommand { get; }

    public Player(int number)
    {
        _name = "Hráč " + number;
        Number = number;
        AddOneCommand = ReactiveCommand.Create<string>(AddOne);
        RemoveOneCommand = ReactiveCommand.Create<string>(RemoveOne);
        money = 20;
        onePercent = LGBT = etnics = smallBussiness = students = elderly = proletariat = families = samozivitele = unemployed = inteligence = agrary = religious = patriots = soldiers = officers = emigrants = 0;

        CardsCollection = new ObservableCollection<Card>();
        Cards = new List<Card>();
        //Cards.Add(new Card("Card 1",1));
    }

    public void PlayCard(Card? card)
    {
        if (card is not null)
        {
            if (Cards.Contains(card))
            {
                return;
            }

            // IF desired to block cards
            //if (card.Cost > money)
            //{
            //    return;
            //}
            AddCardToPlayer(card);
            CardsCollection.Add(card);
        }
        
        foreach (var cardRef in Cards)
        {
            cardRef.Refresh();
        }
        OnPropertyChanged("CardsCollection");
    }
    
    public void RemoveCard(Card? card)
    {
        if (card is not null)
        {
            if (!Cards.Contains(card))
            {
                return;
            }
            money -= card.CostBurn;
            OnPropertyChanged("money");
            Cards.Remove(card);
            CardsCollection.Remove(card);
        }
        
        foreach (var cardRef in Cards)
        {
            cardRef.Refresh();
        }
        OnPropertyChanged("CardsCollection");
    }

    private void AddOne(string column)
    {
        var columnNum = int.Parse(column);
        switch (columnNum)
        {
            case 1:
                ++onePercent;
                OnPropertyChanged("onePercent");
                break;
            case 2:
                ++LGBT;
                OnPropertyChanged("LGBT");
                break;
            case 3:
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
            case 1:
                --onePercent;
                OnPropertyChanged("onePercent");
                break;
            case 2:
                --LGBT;
                OnPropertyChanged("LGBT");
                break;
            case 3:
                --etnics;
                OnPropertyChanged("etnics");
                break;
        }
        
    }

    /**
     * Adds card to player
     */
    public void AddCardToPlayer(Card card)
    {
        Cards.Add(card);
        //OnPropertyChanged("Cards");
        
        money -= card.Cost;
        OnPropertyChanged("money");
    }

    /**
     * Plays cards on new turn
     */
    public void NewRoundPlayer()
    {
        foreach (var card in Cards)
        {
            foreach (var newRoundAction in card.OnRoundStart)
            {
                AddToColumn(newRoundAction);
            }
        }
    }

    public bool TryNewRoundPlayer()
    {
        var MoneyTMP = money;
        foreach (var card in Cards)
        {
            foreach (var newRoundAction in card.OnRoundStart)
            {
                if (newRoundAction.Item1 == 0)
                {
                    MoneyTMP += newRoundAction.Item2;
                }
                
            }
        }

        return MoneyTMP < 0;
    }

    /**
     * Adds specified value to specified column
     */
    private void AddToColumn(Tuple<int, int> pair)
    {
        var columnNum = pair.Item1;
        var value = pair.Item2;
        switch (columnNum)
        {
            case 0:
                money += value;
                OnPropertyChanged("money");
                break;
            case 1:
                onePercent += value;
                if (onePercent <= -10)
                {
                    onePercent = -10; 
                    return;
                }
                OnPropertyChanged("onePercent");
                break;
            case 2:
                LGBT += value;
                if (LGBT <= -18)
                {
                    LGBT = -18; 
                    return;
                }
                OnPropertyChanged("LGBT");
                break;
            case 3:
                etnics += value;
                if (etnics <= -29)
                {
                    etnics = -29; 
                    return;
                }
                OnPropertyChanged("etnics");
                break;
            case 4:
                smallBussiness += value;
                if (smallBussiness <= -37)
                {
                    smallBussiness = -37; 
                    return;
                }
                OnPropertyChanged("smallBussiness");
                break;
            case 5:
                students += value;
                if (students <= -44)
                {
                    students = -44; 
                    return;
                }
                OnPropertyChanged("students");
                break;
            case 6:
                elderly += value;
                if (elderly <= -52)
                {
                    elderly = -52; 
                    return;
                }
                OnPropertyChanged("elderly");
                break;
            case 7:
                proletariat += value;
                if (proletariat <= -46)
                {
                    proletariat = -46; 
                    return;
                }
                OnPropertyChanged("proletariat");
                break;
            case 8:
                families += value;
                if (families <= -38)
                {
                    families = -38; 
                    return;
                }
                OnPropertyChanged("families");
                break;
            case 9:
                samozivitele += value;
                if (samozivitele <= -35)
                {
                    samozivitele = -35; 
                    return;
                }
                OnPropertyChanged("samozivitele");
                break;
            case 10:
                unemployed += value;
                if (unemployed <= -21)
                {
                    unemployed = -21; 
                    return;
                }
                OnPropertyChanged("unemployed");
                break;
            case 11:
                inteligence += value;
                if (inteligence <= -33)
                {
                    inteligence = -33; 
                    return;
                }
                OnPropertyChanged("inteligence");
                break;
            case 12:
                agrary += value;
                if (agrary <= -39)
                {
                    agrary = -39; 
                    return;
                }
                OnPropertyChanged("agrary");
                break;
            case 13:
                religious += value;
                if (religious <= -33)
                {
                    religious = -33; 
                    return;
                }
                OnPropertyChanged("religious");
                break;
            case 14:
                patriots += value;
                if (patriots <= -38)
                {
                    patriots = -38; 
                    return;
                }
                OnPropertyChanged("patriots");
                break;
            case 15:
                soldiers += value;
                if (soldiers <= -13)
                {
                    soldiers = -13; 
                    return;
                }
                OnPropertyChanged("soldiers");
                break;
            case 16:
                emigrants += value;
                if (emigrants <= -5)
                {
                    emigrants = -5; 
                    return;
                }
                OnPropertyChanged("emigrants");
                break;
            case 17:
                officers += value;
                if (officers <= -39)
                {
                    officers = -39; 
                    return;
                }
                OnPropertyChanged("officers");
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