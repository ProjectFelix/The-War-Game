using System;
using System.Threading;

namespace CardDeck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---- Welcome to to my game of War! ----");
            Console.WriteLine("\n\n\t\"War, huh, what is it good for? Absolutely nothing.\" - Edwin Starr\n");
            // Menu
            bool playing = true;
            while (playing) {
                Console.WriteLine("\n-- Menu:");
                Console.WriteLine("-- 1) Start new game");
                Console.WriteLine("-- 2) Exit");
                char menuSelect = (char)Console.ReadKey().Key;
                switch (menuSelect)
                {
                    case '1':
                        NewGame();
                        break;
                    case '2':
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
            

        }

        public static void NewGame()
        {
            // Create decks
            Deck myDeck = new Deck();
            Deck compDeck = new Deck();
            Deck gameDeck = new Deck("game");
            Deck pool = new Deck("pool");

            // Deal deck
            while (gameDeck.NumCards > 0)
            {
                myDeck.AddCard(gameDeck.RemoveTopCard());
                compDeck.AddCard(gameDeck.RemoveTopCard());
            }
            string progress = "";
            while (progress.Length <= 10)
            {
                progress += "-";
                Thread.Sleep(100);
                Console.Clear();
                Console.WriteLine($"\n\n\t\t Dealing deck: [{progress.PadRight(10, ' ')}]");
            }

            // Game variables
            bool playing = true;
            int i = 0;

            // The hands
            Card myCard;
            Card compCard;


            // Start rounds
            while (playing)
            {
                i++;
                Console.Clear();
                Console.WriteLine($"\nRound {i}:");
                if (CheckDeck(ref myDeck, ref playing)) Console.WriteLine("\n-You ran out of cards. You shuffle your winnings into your deck.");
                if (CheckDeck(ref compDeck, ref playing)) Console.WriteLine("\n-The computer ran out of cards. They shuffle their winnings into their deck.");
                if (!playing) break;
                myCard = myDeck.RemoveTopCard();
                compCard = compDeck.RemoveTopCard();
                Console.WriteLine("Your Card:                                         Computer Card:");
                Console.WriteLine($"\t{myCard.ToString().PadRight(18, ' ')} vs\t{compCard}");

                // Evaluate round
                while (myCard == compCard)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t             ~*~*~**~*~*~~*~*~**~*~*~");
                    Console.WriteLine("\t~*~*~**~*~*~     !!!!!!WAARR!!!!!     ~*~*~**~*~*~");
                    Console.WriteLine("\t             ~*~*~**~*~*~~*~*~**~*~*~\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    pool.AddCard(myCard);
                    pool.AddCard(compCard);
                    Console.WriteLine("\nYou and the computer both draw a card from your hands and place it face down on the board.");
                    Console.WriteLine("You both draw a new hand.\n");
                    // Add hand to pool
                    if (CheckDeck(ref myDeck, ref playing)) Console.WriteLine("\n-You ran out of cards. You shuffle your winnings into your deck.");
                    if (CheckDeck(ref compDeck, ref playing)) Console.WriteLine("\n-The computer ran out of cards. They shuffle their winnings into their deck.");
                    if (!playing) break;
                    pool.AddCard(myDeck.RemoveTopCard());
                    pool.AddCard(compDeck.RemoveTopCard());
                    // Get new hand
                    if (CheckDeck(ref myDeck, ref playing)) Console.WriteLine("\n-You ran out of cards. You shuffle your winnings into your deck.");
                    if (CheckDeck(ref compDeck, ref playing)) Console.WriteLine("\n-The computer ran out of cards. They shuffle their winnings into their deck.");
                    if (!playing) break;
                    myCard = myDeck.RemoveTopCard();
                    compCard = compDeck.RemoveTopCard();
                    Console.WriteLine("Your Card:                                         Computer Card:");
                    Console.WriteLine($"             {myCard}  vs  {compCard}");

                }
                if (myCard > compCard && playing)
                {
                    YouWin();
                    myDeck.AddWinnings(myCard);
                    myDeck.AddWinnings(compCard);
                    while (pool.NumCards > 0)
                    {
                        myCard = pool.RemoveTopCard();
                        Console.WriteLine($"You also won: {myCard}");
                        myDeck.AddWinnings(myCard);
                    }
                }
                else if (playing)
                {
                    YouLose();
                    compDeck.AddWinnings(myCard);
                    compDeck.AddWinnings(compCard);
                    while (pool.NumCards > 0)
                    {
                        compCard = pool.RemoveTopCard();
                        Console.WriteLine($"The computer also won: {compCard}");
                        compDeck.AddWinnings(compCard);
                    }
                }
                Console.WriteLine($"\nYour cards remaining: {myDeck.NumCards} \t Winnings: {myDeck.NumWinnings}");
                Console.WriteLine($"Computer cards remaining: {compDeck.NumCards} \t Winnings: {compDeck.NumWinnings}");
                
                // Always making sure we didnt run out of cards
                if (CheckDeck(ref myDeck, ref playing)) Console.WriteLine("\n-You ran out of cards. You shuffle your winnings into your deck.");
                if (CheckDeck(ref compDeck, ref playing)) Console.WriteLine("\n-The computer ran out of cards. They shuffle their winnings into their deck.");

                if (!playing)
                {
                    if (myDeck.NumCards > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n\tThe computer ran out of cards!");
                        Console.WriteLine("\n\tYou beat the computer!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (compDeck.NumCards > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\tYou ran out of cards!");
                        Console.WriteLine("\n\tThe computer beat you!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                Console.WriteLine("\n-Press enter to continue (Press X to exit)");
                char gameInput = (char)Console.ReadKey().Key;
                if (gameInput == 'X') break;

                // This is just to test win condition/shuffling
                if (gameInput == 'T') TestWin(ref compDeck);

                
            }

        }

        public static void TestWin(ref Deck compDeck)
        {
            // For testing purposes - we just throw away the computers cards until they have 5 left.
            compDeck.ShuffleWinnings();
            while (compDeck.NumCards > 5)
            {
                compDeck.RemoveTopCard();
            }
        }

        public static void YouWin()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\t\t ***** You WIN! *****");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void YouLose()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\t\t\t ***** You LOSE! *****");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool CheckDeck(ref Deck deck, ref bool playing)
        {
            
            if (deck.NumCards == 0) // If your deck is out of cards
            {
                if (deck.NumWinnings == 0) // If you have no winnings to shuffle back in
                {
                    playing = false; // game is over
                    return false;
                }
                deck.ShuffleWinnings(); // shuffle your winnings back into your main deck
                return true; // true means we're going to be printing that we shuffled
            }
            return false;
        }
    }
}
