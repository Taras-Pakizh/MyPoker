using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    static class ListExtentions
    {
        public static List<Card> Shuffle(this List<Card> list)
        {
            list = list.OrderBy(a => Guid.NewGuid()).ToList();
            return list;
        }
    }
}
