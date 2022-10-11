namespace Blackjack;

internal class Deck
{
    readonly Stack<Card> _deck = new();
    private List<Card> _tmpDeck = new();

    public Card Draw()
    {
        return _deck.Pop();
    }

    public void Display()
    {
        foreach (var cards in _deck)
            cards.Display();
    }

    public void CreateDeck()
    {
        _deck.Clear();
        var count = 0;
        string suit = "";

        while (count < 4)
        {
            suit = count switch
            {
                0 => "C",
                1 => "S",
                2 => "D",
                3 => "H",
                _ => suit
            };

            for (int i = 2; i < 11; i++)
                _tmpDeck.Add(new(i.ToString(), suit));

            _tmpDeck.Add(new("K", suit));
            _tmpDeck.Add(new("Q", suit));
            _tmpDeck.Add(new("J", suit));
            _tmpDeck.Add(new("A", suit));
            
            count++;
            Shuffle();
            _tmpDeck.ForEach(card => _deck.Push(card));
        }
        
        void Shuffle()
        {
            var shuffle = 0;
            Random slice = new();
            while (shuffle < 101)
            {
                var card = slice.Next(_tmpDeck.Count);
                var card2 = slice.Next(_tmpDeck.Count);

                (_tmpDeck[card], _tmpDeck[card2]) = (_tmpDeck[card2], _tmpDeck[card]);

                shuffle++;
            }
        }
    }
}