using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day07
{
    internal class Sorter: IComparer<Hand>
    {
        public int Compare(Hand? f, Hand? s)
        {
            if (f?.Strength > s?.Strength)
                return 1;

            if (s?.Strength > f?.Strength)
                return -1;

            for (int i = 0; i < f.Cards.Length; i++)
            {
                if (f?.Cards[i].Value > s?.Cards[i].Value)
                    return 1;

                if (s?.Cards[i].Value > f?.Cards[i].Value)
                    return -1;
            }

            return 0;
        }
    }
}
