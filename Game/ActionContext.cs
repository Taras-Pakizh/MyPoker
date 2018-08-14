using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Game
{
    class ActionContext
    {
        public Action action { get; private set; }
        public int bet { get; private set; }

        public ActionContext(Action _action, int _bet)
        {
            action = _action;
            bet = _bet;
        }
    }
}
