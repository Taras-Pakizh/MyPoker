using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    class Hand : IComparable<Hand>
    {
        //Vars
        private List<Card> cards;
        private AbstractHandRank rank;
        private bool modified;

        //Constructor
        public Hand()
        {
            cards = new List<Card>();
            modified = true;
        }
        public Hand(Card[] hand)
        {
            cards = new List<Card>(hand);
            rank = new HandRank(hand);
            modified = false;
        }

        //Properties
        public AbstractHandRank Rank
        {
            get
            {
                if (!modified) return rank;
                else
                {
                    rank = new HandRank(cards.ToArray());
                    modified = false;
                    return rank;
                }
            }
        }

        //IComparable
        public int CompareTo(Hand other)
        {
            return Rank.CompareTo(other.Rank);
        }

        //Methods
        public void AddCards(Card[] _cards)
        {
            cards.AddRange(_cards);
            modified = true;
        }
        public void Clear()
        {
            cards.Clear();
            modified = true;
        }
    }
}
