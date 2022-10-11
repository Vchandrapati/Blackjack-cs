using System.Reflection.Metadata.Ecma335;

namespace Blackjack;

internal class Card
{
    public readonly string Name;
    public readonly string Suit;

    public Card(string name, string suit)
    {
        Name = name;
        Suit = suit;
    }

    public string Display()
    {
        return(Name != "pip" ? $@"{Name}{Suit} " : $@"{Value()}{Suit} ");
    }

    public int Value()
    {
        try
        {
            var value = Convert.ToInt32(Name);
            return value;
        }
        catch
        {
            return Name == "Ace" ? 11 : 10;
        }
    }
}