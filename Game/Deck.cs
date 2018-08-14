using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    class Deck : IDeck
    {
        //Vars
        private static readonly List<Card> AllCards;
        private readonly List<Card> deck;
        private int index;

        //Constructor
        static Deck()
        {
            AllCards = new List<Card>();
            for (int suit = 0; suit < 4; ++suit)
                for (int type = 2; type < 15; ++type)
                    AllCards.Add(new Card((CardSuit)suit, (CardType)type));
        }
        public Deck()
        {
            index = -1;
            deck = AllCards.Shuffle().ToList();
        }

        /// <summary>
        /// Interface IDeck
        /// </summary>
        /// <returns></returns>
        public Card GetNextCard()
        {
            if (++index >= deck.Count) throw new EndOfDeck();
            return deck[index];
        }
        public Card[] GetNextCards(int count)
        {
            if (++index + count >= deck.Count) throw new EndOfDeck();
            Card[] result = deck.GetRange(index, count).ToArray();
            index += count - 1;
            return result;
        }
        public bool IsEmpty()
        {
            if (index < deck.Count - 1) return false;
            else return true;
        }

        /// <summary>
        /// Exceptions
        /// </summary>
        class EndOfDeck : Exception { }
    }
}
