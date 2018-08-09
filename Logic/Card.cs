using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    public class Card:IComparable<Card>
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

        /// <summary>
        /// Compares only type of card
        /// </summary>
        /// <param name="other"></param>
        /// <returns> Less than zero = Upper type </returns>
        public int CompareTo(Card other)
        {
            return -((int)type).CompareTo((int)other.type);
        }
    }
}
