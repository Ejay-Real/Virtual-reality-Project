using System;

class program
{

    static void Main(string[] args)
    {
        List<string> deck = InitializeDeck();
        shuffle(deck);

        List<string> playerHand = new List<string> { dealCard(deck), dealCard(deck) };
        List<string> dealerHand = new List<string> { dealCard(deck), dealCard(deck) };

        Console.WriteLine("Player's Hand:");

        foreach (var card in playerHand)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine($"Player's Total: {CalculateHandValue(playerHand)}\n");

        Console.WriteLine("Dealer's hand");
        Console.WriteLine(dealerHand[0]);
        bool bust = false;

        while (true)
        {
            Console.WriteLine("Hit or stand? (h/s): ");
            string action = Console.ReadLine().ToLower();

            if (action == "h")
            {
                playerHand.Add(dealCard(deck));
                Console.WriteLine("\nPllayer's Hand:");
                foreach (var card in playerHand)
                {
                    Console.WriteLine($"{card}");
                }
                int playerTotal = CalculateHandValue(playerHand);
                Console.WriteLine($"Player's Total: {CalculateHandValue(playerHand)}\n");

                if (playerTotal > 21)
                {
                    Console.WriteLine("Player Busted. Dealer wins.");
                    bust = true;
                    break;
                }
            }
            else if (action == "s")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Choice please select 'h' for hit or 's' for stand");
            }
        }
        if (!bust)
        {
            Console.WriteLine("Dealers turn: ");
            Console.WriteLine("Dealers hand: ");

            foreach (var card in dealerHand)
            {
                Console.WriteLine(card);
            }

            while (CalculateHandValue(dealerHand) < 17)
            {
                dealerHand.Add(dealCard(deck));
                Console.WriteLine("\nDealer hits and receives a card.");

                foreach (var card in dealerHand)
                {
                    Console.WriteLine(card);
                }
            }
            int dealerTotal = CalculateHandValue(dealerHand);
            Console.WriteLine($"\nDealer's total: {dealerTotal}");

            int playerTotal = CalculateHandValue(playerHand);
            if (dealerTotal > 21)
            {
                Console.WriteLine("Dealer busted! Player wins");
            }
            else if (playerTotal > dealerTotal)
            {
                System.Console.WriteLine("Player Wins!");
            }
            else if (playerTotal < dealerTotal)
            {
                Console.WriteLine("Dealer Wins!");
            }
            else
            {
                Console.WriteLine("It's a push!");
            }
        }
    }
    static List<String> InitializeDeck()
    {
        List<string> deck = new List<string>();
        string[] suits = { "Hearts", "Spades", "Clubs", "Diamonds" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace"};

        foreach (var suit in suits)
        {
            foreach (var rank in ranks)
            {
                deck.Add($"{rank} of {suit}");
            }

        }
        return deck;

    }
    static void shuffle(List<string> deck)
    {
        Random random = new Random();

        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = random.Next(1 + i);
            var temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }

    }

    static string dealCard(List<string> deck)
    {

        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    static int CalculateHandValue(List<string> hand)
    {
        int total = 0;
        int aceCount = 0;

        foreach (var card in hand)
        {
            string rank = card.Split(' ')[0];
            if (int.TryParse(rank, out int numericValue))
            {
                total += numericValue;
            }
            else if (rank == "Jack" || rank == "Queen" || rank == "King")
            {
                total += 10;
            }
            else if (rank == "Ace")
            {
                aceCount++;
                total += 11;
            }
        }
        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }
        return total;
    }
}