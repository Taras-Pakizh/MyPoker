using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    class Card:IComparable<Card>
    {
        //Properties
        public CardType type { get; }
        public CardSuit suit { get; }

        //Constructors
        public Card(CardSuit _suit, CardType _type)
        {
            type = _type;
            suit = _suit;
        }

        public int CompareTo(Card other)
        {
            return -((int)type).CompareTo((int)other.type);
        }
    }
}
