using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Game
{
    class Board
    {
        private IDeck deck;
        private List<IPlayer> players;

        public Board()
        {
            deck = new Deck();
        }

        public void StartGame(int countOfPlayers, int dib)
        {
            players = new List<IPlayer>();
            players.Add(new Player(dib));
            for (int i = 1; i < countOfPlayers; ++i)
                players.Add(new Bot());

        }
    }
}
