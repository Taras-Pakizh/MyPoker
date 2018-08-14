using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPoker.Logic;

namespace MyPoker.Game
{
    public static class ListExtentions
    {
        public static List<Card> Shuffle(this List<Card> list)
        {
            list = list.OrderBy(a => Guid.NewGuid()).ToList();
            return list;
        }
    }
}
