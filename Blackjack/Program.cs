using Blackjack;
using static System.Console;

Deck shoe = new();
bool dealerStand = false, playerStand = false;
Start();


List<Card> player;
List<Card> dealer;

void Start()
{
    WriteLine("Start new Game? \nYes? No?");
    var input = ReadLine()?.ToLower();
    if(input != "yes" && input != "no") 
        Start();
    else if (input == "no")
    {
        Environment.Exit(0);
        return;
    }

    shoe.CreateDeck();
    player = new List<Card>();
    dealer = new List<Card>();

    for (var i = 0; i < 4; i++)
    {
        if (i % 2 == 0)
            dealer.Add(shoe.Draw());
        else
            player.Add(shoe.Draw());
    }
    Play();
}

void Play()
{
    while (!playerStand && !dealerStand)
    {
        if(!playerStand)
        {
            WriteLine();
            DisplayHands();
            WriteLine("Hit or Stand");
            var input = ReadLine()?.ToLower();
            switch (input)
            {
                case "hit":
                {
                    player.Add(shoe.Draw());

                    if (EvaluateHand(player)) continue;
                    DisplayWinner("Dealer");
                    Start();
                    break;
                }
                case "stand":
                    playerStand = true;
                    break;
            }
        }

        var handValue = 0;
        dealer.ForEach(card => handValue += card.Value());
        switch (handValue)
        {
            case < 17:
                dealer.Add(shoe.Draw());
                if (!EvaluateHand(dealer))
                    DisplayWinner("Player");
                break;
            case > 21:
                DisplayWinner("Player");
                break;
            case >= 17:
                dealerStand = true;
                break;
        }
    }
    End();
}

bool EvaluateHand(List<Card> hand)
{
    var sum = 0;
    hand.ForEach(card => sum += card.Value());
    if (hand.Contains(new Card("A", "S")) || hand.Contains(new Card("A", "D")) || 
        hand.Contains(new Card("A", "H")) || hand.Contains(new Card("A", "C")))
    {
        return sum < 21 && sum - 10 < 21;
    }

    return sum < 21;
}

void DisplayHands()
{
    var playerHand = "";
    player.ForEach(card => playerHand += card.Display());
    WriteLine(
        $@"Dealer Hand: {dealer[0].Display()}{string.Concat(Enumerable.Repeat("X ", dealer.Count - 1))}");
    WriteLine($@"Your Hand: {playerHand}");
    WriteLine();
}

void End()
{
    var playerHandValue = 0;
    var dealerHandValue = 0;
    
    player.ForEach(card => playerHandValue += card.Value());
    dealer.ForEach(card => dealerHandValue += card.Value());

    if (playerHandValue > dealerHandValue)
        DisplayWinner("Player");
    else if(playerHandValue < dealerHandValue)
        DisplayWinner("Dealer");
    
    else if (playerHandValue == dealerHandValue)
    {
        if (player.Contains(new Card("J", "C")) || player.Contains(new Card("J", "S")))
            DisplayWinner("Player Wins");
        else if (dealer.Contains(new Card("J", "C")) || dealer.Contains(new Card("J", "S")))
            DisplayWinner("Dealer Wins");
        else
        {
            WriteLine();
            WriteLine("Draw");
            var hand = "";
            dealer.ForEach(card => hand += card.Display());
            WriteLine($@"Dealers Hand: {hand}");
    
            hand = "";
            player.ForEach(card => hand += card.Display());
            WriteLine($@"Your Hand: {hand}" + '\n');
        }
        
        Start();
    }
}

void DisplayWinner(string winner)
{
    WriteLine();
    WriteLine(winner == "Dealer" ? "Dealer Wins" : "Player Wins");
    var hand = "";
    
    dealer.ForEach(card => hand += card.Display());
    WriteLine($@"Dealers Hand: {hand}");
    
    hand = "";
    player.ForEach(card => hand += card.Display());
    WriteLine($@"Your Hand: {hand}" + '\n');
    Environment.Exit(0);
}