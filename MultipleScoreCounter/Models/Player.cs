using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultipleScoreCounter.Models;

public class Player
{
    public int Number { get; set; }
    public string Name
    {
        get => "Hráč " + Number;
    }

    public Player(int number)
    {
        Number = number;
    }
    
}