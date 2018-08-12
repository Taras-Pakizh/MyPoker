using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPoker.Logic;

namespace MyPokerTest
{
    [TestClass]
    public class HandRankTest
    {
        [TestMethod]
        public void FlushStraightTest()
        {
            //arrange
            Card[] hand = new Card[7]
            {
                new Card(CardSuit.Club, CardType.King),
                new Card(CardSuit.Club, CardType.Nine),
                new Card(CardSuit.Club, CardType.Ten),
                new Card(CardSuit.Club, CardType.Jack),
                new Card(CardSuit.Club, CardType.Two),
                new Card(CardSuit.Heart, CardType.Two),
                new Card(CardSuit.Club, CardType.Queen)
            };
            Card expectedHighCard = new Card(CardSuit.Club, CardType.King);
            Combination expectedCombination = Combination.Straight_flush;

            //act
            var result = new HandRank().Parse_RoyalStraightFlush_Flush(hand);
            Combination actualCombination = result.Item1;
            Card actualHighCard = result.Item2;

            //assert
            Assert.AreEqual(expectedCombination, actualCombination);
            Assert.AreEqual(expectedHighCard.suit, actualHighCard.suit);
            Assert.AreEqual(expectedHighCard.type, actualHighCard.type);
        }
    }
}
