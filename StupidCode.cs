using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars
{
    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public class PokerHand
    {
        public Card[] cards;
        public ShowDown Value;

        public PokerHand(string hand)
        {
            cards = new Card[5];
            string[] StrCards = hand.Split(new char[] { ' ' });
            for (int i = 0; i < cards.Length; ++i)
                cards[i] = new Card(StrCards[i]);
            Value = new ShowDown(cards);
        }

        public Result CompareWith(PokerHand hand)
        {
            Result result = Result.Win;
            if (Value.value < hand.Value.value) result = Result.Win;
            else if (Value.value > hand.Value.value) result = Result.Loss;
            else if (Value.value == hand.Value.value)
            {
                if (Value.top > hand.Value.top) result = Result.Win;
                else if (Value.top < hand.Value.top) result = Result.Loss;
                else if (Value.top == hand.Value.top)
                {
                    int i = ShowDown.Compare(cards, hand.cards);
                    if (i > 0) result = Result.Win;
                    else if (i < 0) result = Result.Loss;
                    else if (i == 0) result = Result.Tie;
                }
            }
            return result;
        }
    }

    public class Card
    {
        public int rank { get; private set; }
        public int suit { get; private set; }
        public Card(string card)
        {
            int value;
            if (Int32.TryParse(card[0].ToString(), out value)) rank = value;
            else
            {
                if (card[0] == 'T') rank = 10;
                else if (card[0] == 'J') rank = 11;
                else if (card[0] == 'Q') rank = 12;
                else if (card[0] == 'K') rank = 13;
                else if (card[0] == 'A') rank = 14;
            }

            if (card[1] == 'S') suit = 1;
            else if (card[1] == 'H') suit = 2;
            else if (card[1] == 'D') suit = 3;
            else if (card[1] == 'C') suit = 4;
        }
    }

    public class ShowDown
    {
        public int top { get; private set; }
        public int value { get; private set; }

        public ShowDown(Card[] cards)
        {
            if (!Street_Fleash(cards))
                if (!Karre(cards))
                    if (!Full_House(cards))
                        if (!Fleash(cards))
                            if (!Street(cards))
                                if (!Three(cards))
                                    if (!TwoPairs(cards))
                                        if (!Pair(cards))
                                            Top(cards);
        }

        //Combinations
        private bool Street_Fleash(Card[] cards)
        {
            if (Fleash(cards)) if (Street(cards)) { value = 0; return true; }
            return false;
        }
        private bool Street(Card[] cards)
        {
            //Check if they are all different
            List<int> list = new List<int>();
            for (int i = 0; i < cards.Length; ++i)
            {
                if (list.Contains(cards[i].rank)) return false;
                list.Add(cards[i].rank);
            }

            //Check if street
            top = list.Max();
            bool result = true;
            if (top == 14)
            {
                for (int i = 2; i < 6; ++i)
                    if (!list.Contains(i)) { result = false; break; }
                if (result == true) return true;
            }
            result = true;
            for (int i = top; i > top - 5; --i)
                if (!list.Contains(i)) { result = false; break; }

            if (result == true) value = 4;
            return result;
        }
        private bool Fleash(Card[] cards)
        {
            for (int i = 1; i < cards.Length; ++i)
                if (cards[0].suit != cards[i].suit) return false;
            value = 3;
            return true;
        }
        private bool Karre(Card[] cards)
        {
            for (int i = 0; i < cards.Length; ++i)
                for (int index = 0, sum = 1; index < cards.Length; ++index)
                {
                    if (index == i) continue;
                    if (cards[i].rank == cards[index].rank) sum++;
                    if (sum == 4) { top = cards[i].rank; value = 1; return true; }
                }
            return false;
        }
        private bool Full_House(Card[] cards)
        {
            bool pair = false; bool three = false; int pairVal = -5; int threeVal = -2;
            for (int i = 0; i < 5; ++i)
                for (int index = 0, sum = 1; index < 5; ++index)
                {
                    if (index == i) continue;
                    if (cards[i].rank == cards[index].rank) sum++;
                    if (sum == 2) if (pair == false) { pair = true; pairVal = cards[i].rank; }
                    if (sum == 3) { three = true; threeVal = cards[i].rank; if (threeVal == pairVal) pair = false; }
                    if (pair && three && pairVal != threeVal)
                    {
                        top = pairVal;
                        if (top < threeVal) top = threeVal;
                        value = 2;
                        return true;
                    }
                }
            return false;
        }
        private bool Three(Card[] cards)
        {
            for (int i = 0; i < cards.Length; ++i)
                for (int index = 0, sum = 1; index < cards.Length; ++index)
                {
                    if (index == i) continue;
                    if (cards[i].rank == cards[index].rank) sum++;
                    if (sum == 3) { top = cards[i].rank; value = 5; return true; }
                }
            return false;
        }
        private bool TwoPairs(Card[] cards)
        {
            bool pair1 = false; bool pair2 = false; int pairVal1 = -5; int pairVal2 = -2;
            for (int i = 0; i < 5; ++i)
                for (int index = 0, sum = 1; index < 5; ++index)
                {
                    if (index == i) continue;
                    if (cards[i].rank == cards[index].rank) sum++;
                    if (sum == 2)
                    {
                        if (!pair1) { pair1 = true; pairVal1 = cards[index].rank; }
                        else if (!pair2 && pairVal1 != cards[index].rank) { pair2 = true; pairVal2 = cards[index].rank; }
                    }
                    if (pair1 && pair2 && pairVal1 != pairVal2)
                    {
                        top = pairVal1; if (top < pairVal2) top = pairVal2;
                        value = 6;
                        return true;
                    }
                    else if (sum == 2) break;
                }
            return false;
        }
        private bool Pair(Card[] cards)
        {
            for (int i = 0; i < cards.Length; ++i)
                for (int index = 0, sum = 1; index < cards.Length; ++index)
                {
                    if (index == i) continue;
                    if (cards[i].rank == cards[index].rank) sum++;
                    if (sum == 2) { top = cards[i].rank; value = 7; return true; }
                }
            return false;
        }
        private void Top(Card[] cards)
        {
            value = 8;
            int max = cards[0].rank;
            for (int i = 1; i < 5; ++i)
                if (max < cards[i].rank) max = cards[i].rank;
            top = max;
        }

        //Compare cards
        public static int Compare(Card[] cards1, Card[] cards2)
        {
            int[][] matrix = new int[2][];
            matrix[0] = new int[5]; matrix[1] = new int[5];
            for (int i = 0; i < 5; ++i)
            {
                matrix[0][i] = cards1[i].rank;
                matrix[1][i] = cards2[i].rank;
            }
            Array.Sort(matrix[0]); Array.Sort(matrix[1]);
            for (int i = 4; i > -1; --i)
            {
                if (matrix[0][i] < matrix[1][i]) return -1;
                else if (matrix[0][i] > matrix[1][i]) return 1;
            }
            return 0;
        }
    }
}
