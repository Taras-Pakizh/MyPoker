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

        /// <summary>
        /// Define the combination of hand and highCard
        /// </summary>
        /// <param name="hand"></param>
        private void ParseHand(Card[] hand)
        {

            //Pairs
            Dictionary<Card, int> counts = new Dictionary<Card, int>();
            foreach (var card in hand)
                counts.Add(card, hand.Count(x => x == card));
        }

        private bool ParseFlush(Card[] hand, out CardSuit flushSuit)
        {
            Dictionary<CardSuit, int> suits = new Dictionary<CardSuit, int>();
            foreach (var suit in new CardSuit[] { CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade })
                suits.Add(suit, hand.Count(x => x.suit == suit));
            if (suits.ContainsValue(5))
            {
                CardSuit mysuit = suits.Where(x => x.Value == 5).Single().Key;
                var combination = hand.Where(x => x.suit == mysuit).OrderByDescending(x => (int)x.type).ToList();
                highCard = combination.First();
                bool straight = true;
                for (int type = (int)highCard.type, i = 1; i < 5; ++i)
                    if ((int)combination[i].type != --type)
                    {
                        straight = false;
                        break;
                    }
            }
        }
        private void ParseStraight()
        {

        }
    }
}
