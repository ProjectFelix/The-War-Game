using System;
using System.Collections.Generic;

namespace CardDeck
{
    class Deck
    {
        List<Card> Cards { get; set; }
        List<Card> Winnings { get; set; }
        public int NumCards { get => Cards.Count; }
        public int NumWinnings { get => Winnings.Count; }

        public Deck()
        {
            Cards = new List<Card>();
            Winnings = new List<Card>();
            
        }

        public Deck(string name)
        {
            Cards = new List<Card>();

            if (name == "game")
            {
                foreach (CardSuit cs in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardValue cv in Enum.GetValues(typeof(CardValue)))
                    {
                        this.AddCard(new Card(cs, cv));
                    }
                }
                this.ShuffleDeck();
            }
        }

        public void AddCard(Card newCard)
        {
            Cards.Add(newCard);
        }
        public void AddWinnings(Card newCard)
        {
            Winnings.Add(newCard);
        }

        public Card RemoveTopCard()
        {
            Card cardToRemove = Cards[0];
            Cards.RemoveAt(0);
            return cardToRemove;
        }

        public void PrintDeck()
        {
            foreach(Card c in Cards)
            {
                System.Console.WriteLine(c);
            }
        }

        public void SortDeck()
        {
            Cards.Sort();
        }

        public void ShuffleDeck()
        {
            List<Card> temp = new List<Card>();
            Random rand = new Random();
            while (NumCards > 0)
            {
                int i = rand.Next(0, NumCards);
                temp.Add(Cards[i]);
                Cards.RemoveAt(i);
            }
            this.Cards = temp;
        }

        public void ShuffleWinnings()
        {
            Cards = Winnings;
            this.ShuffleDeck();
            Winnings = new List<Card>();
        }
    }
}