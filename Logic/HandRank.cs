using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    class HandRank : IComparable<HandRank>
    {
        //Vars
        private Combination combination;
        private Card highCard;

        //Constructor
        public HandRank(Card[] hand)
        {
            ParseHand(hand);
        }

        public int CompareTo(HandRank other)
        {
            if ((int)combination < (int)other.combination) return -1;
            else if ((int)combination > (int)other.combination) return 1;
            else return highCard.CompareTo(other.highCard);
        }

        private void ParseHand(Card[] hand)
        {

        }
    }
}
