using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    delegate ActionContext PlayerTurnEventHandler(object sender, GameContext g);

    class Player:IPlayer
    {
        public Hand hand { get; private set; }
        public int dib { get; private set; }

        public event PlayerTurnEventHandler PlayerTurn;

        public Player(int _dib)
        {
            dib = _dib;
            hand = new Hand();
        }

        public ActionContext Blind(int blind)
        {
            if (blind > dib) return new ActionContext(Action.End_of_game, 0);
            dib -= blind;
            return new ActionContext(Action.Bet, blind);
        }
        public ActionContext Turn(GameContext gameContext)
        {
            if (PlayerTurn == null) throw new NotImplementedException();
            return PlayerTurn.Invoke(this, gameContext);
        }
        public void UpdateHand(Card[] cards)
        {
            hand.AddCards(cards);
        }
    }
}
