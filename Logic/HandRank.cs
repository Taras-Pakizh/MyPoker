using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    public class HandRank : IComparable<HandRank>
    {
        //Vars
        private readonly Combination combination;
        private readonly Card highCard;
        private readonly Card[] SortedHand;

        //Constructor
        public HandRank(){}
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

        public (Combination, Card) Parse_RoyalStraightFlush_Flush(Card[] hand)
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
            Combination result = Combination.None_Found;
            Card straightHighCard = null;
            Array.Sort(hand);
            hand = hand.Reverse().ToArray();
            Card item = hand.First();
            int start = 0, length = 1;
            for(int i = 1; i < hand.Length; ++i)
            {
                if(hand[i].type == item.type + 1) ++length;
                else
                {
                    length = 1;
                    start = i;
                }
                item = hand[i];
                if(length == 4 && item.type == CardType.Five && hand.Contains(new Card(CardSuit.Club, CardType.Ace)))
                {
                    result = Combination.Straight;
                    straightHighCard = hand.First(x => x.type == CardType.Ace);
                    break;
                }
            }
            if(length >= 5)
            {
                result = Combination.Straight;
                straightHighCard = hand[start + length - 1];
            }
            return (result, straightHighCard);
        }
        private (Combination, Card) ParsePairs(Card[] hand)
        {
            Combination result = Combination.None_Found;
            Card PairsHighCard = null;
            Dictionary<CardType, int> dublicates = new Dictionary<CardType, int>();
            foreach(var card in hand)
            {
                if (dublicates.ContainsKey(card.type))
                    ++dublicates[card.type];
                else dublicates.Add(card.type, 1);
            }
            if (dublicates.ContainsValue(4))
            {
                result = Combination.Quads;
                PairsHighCard = new Card(CardSuit.Club, dublicates.Where(x => x.Value == 4).First().Key);
            }
            else if (dublicates.ContainsValue(3))
            {
                if (dublicates.ContainsValue(2))
                    result = Combination.Full_house;
                else result = Combination.Set;
                PairsHighCard = new Card(CardSuit.Club, dublicates.Where(x => x.Value == 3).Max(x => x.Key));
            }
            else if(dublicates.ContainsValue(2))
            {
                var pairs = dublicates.Where(x => x.Value == 2).Select(x => x.Key).ToArray();
                if (pairs.Length == 1) result = Combination.One_Pair;
                else result = Combination.Two_Pairs;
                PairsHighCard = new Card(CardSuit.Club, pairs.Max());
            }
            return (result, PairsHighCard);
        }
    }
}
