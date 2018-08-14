using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    class Bot : IPlayer
    {
        public ActionContext Blind(int blind)
        {
            throw new NotImplementedException();
        }

        public ActionContext Turn(GameContext gameContext)
        {
            throw new NotImplementedException();
        }

        public void UpdateHand(Card[] cards)
        {
            throw new NotImplementedException();
        }
    }
}
