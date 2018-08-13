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
        private readonly Card[] SortedHand;

        //Constructor
        public HandRank(){}
        public HandRank(Card[] hand)
        {
            if (hand.Length < 5) throw new NotEnoughCards();
            var result = Parse_RoyalStraightFlush_Flush(hand);
            if (result.Item1 == Combination.None_Found)
                result = ParseStraight(hand);
            if (result.Item1 > Combination.Straight_flush)
            {
                var check = ParsePairs(hand);
                if (result.Item1 > check.Item1)
                    result = check;
            }
            combination = result.Item1;
            SortedHand = result.Item2;
        }

        //Comparable interface
        public int CompareTo(HandRank other)
        {
            if (combination < other.combination) return -1;
            else if (combination > other.combination) return 1;
            else
            {
                for(int i = 0, result; i < SortedHand.Length; ++i)
                {
                    result = this.SortedHand[i].CompareTo(other.SortedHand[i]);
                    if (result != 0) return result;
                }
                return 0;
            }
        }

        //Parsers
        public (Combination, Card[]) Parse_RoyalStraightFlush_Flush(Card[] hand)
        {
            Combination result = Combination.None_Found;
            Card[] sortedHand = new Card[5];
            Dictionary<CardSuit, int> suits = new Dictionary<CardSuit, int>();
            foreach (var suit in new CardSuit[] { CardSuit.Club, CardSuit.Diamond, CardSuit.Heart, CardSuit.Spade })
                suits.Add(suit, hand.Count(x => x.suit == suit));
            int maxValue = suits.Values.Max();
            if (maxValue >= 5)
            {
                CardSuit flushSuit = suits.Where(x => x.Value == maxValue).Single().Key;
                var flushHand = hand.Where(x => x.suit == flushSuit).OrderByDescending(x => (int)x.type).ToArray();
                result = Combination.Flush;
                sortedHand = flushHand.Take(sortedHand.Length).ToArray();
                var straightParseResult = ParseStraight(flushHand);
                if (straightParseResult.Item1 != Combination.None_Found)
                {
                    if (straightParseResult.Item2.type == CardType.Ace) result = Combination.Royal_flush;
                    else result = Combination.Straight_flush;
                    sortedHand = straightParseResult.Item2;
                }
            }
            return (result, sortedHand);
        }
        public (Combination, Card[]) ParseStraight(Card[] hand)
        {
            Combination result = Combination.None_Found;
            List<Card> sortedHand = new List<Card>();
            Array.Sort(hand);
            hand = hand.Reverse().ToArray();
            Card item = hand.First();
            sortedHand.Add(hand[0]);
            for(int i = 1; i < hand.Length; ++i)
            {
                if (hand[i].type == item.type + 1) sortedHand.Add(hand[i]);
                else if (sortedHand.Count < 5)
                {
                    sortedHand.Clear();
                    sortedHand.Add(hand[i]);
                }
                else break;
                item = hand[i];
                if(sortedHand.Count == 4 && item.type == CardType.Five && hand.Contains(new Card(CardSuit.Club, CardType.Ace)))
                {
                    result = Combination.Straight;
                    sortedHand = sortedHand.OrderByDescending(x=>x.type).ToList();
                    sortedHand.Add(hand.First(x => x.type == CardType.Ace));
                    break;
                }
            }
            if(sortedHand.Count >= 5)
            {
                result = Combination.Straight;
                sortedHand = sortedHand.OrderByDescending(x => x.type).Take(5).ToList();
            }
            return (result, sortedHand.ToArray());
        }
        public (Combination, Card[]) ParsePairs(Card[] hand)
        {
            Combination result = Combination.None_Found;
            List<Card> sortedHand = new List<Card>();
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
                sortedHand.AddRange(hand.Where(x => x.type == dublicates.Where(y => y.Value == 4).First().Key));
                Card card = hand.Max();
                if (card.type != sortedHand[0].type)
                    sortedHand.Add(card);
                else sortedHand.Add(hand.Where(x => x.type != sortedHand[0].type).Max());
            }
            else if (dublicates.ContainsValue(3))
            {
                CardType set = dublicates.Where(x => x.Value == 3).Max(x => x.Key);
                sortedHand.AddRange(hand.Where(x => x.type == set));
                if (dublicates.ContainsValue(2))
                {
                    result = Combination.Full_house;
                    CardType pair = dublicates.Where(x => x.Value == 2).Max(x => x.Key);
                    sortedHand.AddRange(hand.Where(x => x.type == pair));
                }
                else
                {
                    result = Combination.Set;
                    sortedHand.AddRange(hand.Where(x => x.type != set).OrderByDescending(x => x.type).Take(2));
                }
            }
            else if(dublicates.ContainsValue(2))
            {
                var pairs = dublicates.Where(x => x.Value == 2).Select(x => x.Key).OrderByDescending(x=>x).Take(2).ToArray();
                var rest = hand.Where(x => x.type != pairs[0]).Where(y => y.type != pairs[1]).OrderByDescending(x => x).ToArray();
                if (pairs.Length == 1) result = Combination.One_Pair;
                else result = Combination.Two_Pairs;
                foreach (var type in pairs)
                    sortedHand.AddRange(hand.Where(x => x.type == type));
                sortedHand.AddRange(rest.Take(5 - sortedHand.Count));
            }
            return (result, sortedHand.ToArray());
        }

        class NotEnoughCards : Exception { }
    }
}
