using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    interface IDeck
    {
        bool IsEmpty();
        Card GetNextCard();
        Card[] GetNextCards(int count);
    }
}
