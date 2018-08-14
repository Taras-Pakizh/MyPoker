using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    interface IPlayer
    {
        void UpdateHand(Card[] cards);
        ActionContext Turn(GameContext gameContext);
        ActionContext Blind(int blind);
    }
}
