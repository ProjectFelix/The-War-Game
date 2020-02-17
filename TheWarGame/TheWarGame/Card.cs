using System;

namespace CardDeck
{
    public struct Card : IComparable<Card>
    {
        CardSuit Suit { get; set; }
        CardValue Value { get; set; }

        public Card(CardSuit suit, CardValue value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public int CompareTo(Card otherCard)
        {
            if(this.Value == otherCard.Value)
                return 0;
            return (this.Value > otherCard.Value) ? 1 : -1;
        }

        public override bool Equals(object otherCard)
        {
            if(otherCard == null)
                return false;
            return ((Card)otherCard).Value == this.Value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator >  (Card operand1, Card operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }
        
        public static bool operator <  (Card operand1, Card operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        public static bool operator >=  (Card operand1, Card operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }
        
        public static bool operator <=  (Card operand1, Card operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }
        
        public static bool operator ==  (Card operand1, Card operand2)
        {
            return operand1.Equals(operand2);
        }

        public static bool operator !=  (Card operand1, Card operand2)
        {
            return !operand1.Equals(operand2);
        }
    }
}