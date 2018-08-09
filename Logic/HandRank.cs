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
        private readonly Combination combination;
        private readonly Card highCard;

        //Constructor
        public HandRank(Card[] hand)
        {
            var result = Parse_RoyalStraightFlush_Flush(hand);
            if (result.Item1 == Combination.None_Found)
                result = ParseStraight(hand);
            if ((int)result.Item1 > 1)
            {
                var check = ParsePairs(hand);
                if (result.Item1 > check.Item1)
                    result = check;
            }
            combination = result.Item1;
            highCard = result.Item2;
        }

        public int CompareTo(HandRank other)
        {
            if ((int)combination < (int)other.combination) return -1;
            else if ((int)combination > (int)other.combination) return 1;
            else return highCard.CompareTo(other.highCard);
        }

        private (Combination, Card) Parse_RoyalStraightFlush_Flush(Card[] hand)
        {
            Combination result = Combination.None_Found;
            Card flushHighCard = null;
            Dictionary<CardSuit, int> suits = new Dictionary<CardSuit, int>();
            foreach (var suit in new CardSuit[] { CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade })
                suits.Add(suit, hand.Count(x => x.suit == suit));
            int maxValue = suits.Values.Max();
            if (maxValue >= 5)
            {
                CardSuit flushSuit = suits.Where(x => x.Value == maxValue).Single().Key;
                var flushHand = hand.Where(x => x.suit == flushSuit).OrderByDescending(x => (int)x.type).ToArray();
                result = Combination.Flush;
                flushHighCard = flushHand.First();
                if (ParseStraight(flushHand).Item1 != Combination.None_Found)
                {
                    if (flushHighCard.type == CardType.Ace) result = Combination.Royal_flush;
                    else result = Combination.Straight_flush;
                }
            }
            return (result, flushHighCard);
        }
        private (Combination, Card) ParseStraight(Card[] hand)
        {
            
        }
        private (Combination, Card) ParsePairs(Card[] hand)
        {
            //Pairs
            Dictionary<Card, int> counts = new Dictionary<Card, int>();
            foreach (var card in hand)
                counts.Add(card, hand.Count(x => x == card));
        }
    }
}
