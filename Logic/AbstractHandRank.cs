using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoker.Logic
{
    public abstract class AbstractHandRank : IComparable<AbstractHandRank>
    {
        public Combination _combination { get; protected set; }
        public IReadOnlyList<Card> _sortedHand { get; protected set; }

        public int CompareTo(AbstractHandRank other)
        {
            this.GetValues();
            other.GetValues();
            if (_combination == Combination.None_Found || other._combination == Combination.None_Found)
                throw new CombinationNotFound();
            if (_combination < other._combination) return -1;
            else if (_combination > other._combination) return 1;
            else
            {
                for (int i = 0, result; i < _sortedHand.Count; ++i)
                {
                    result = this._sortedHand[i].CompareTo(other._sortedHand[i]);
                    if (result != 0) return result;
                }
                return 0;
            }
        }

        protected abstract void GetValues();

        class CombinationNotFound : Exception { }
    }
}
