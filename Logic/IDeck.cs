using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    interface IDeck
    {
        bool IsEmpty();
        Card GetNextCard();
        Card[] GetNextCards(int count);
    }
}
