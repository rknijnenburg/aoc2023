using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day07
{
    internal class Hand
    {
        public Card[] Cards { get; }

        public int Bid { get; }

        public int Strength { get; }

        public Hand(string line)
        {
            var parts = line.Split(" ");

            Bid = Convert.ToInt32(parts[1].Trim());
            Cards = parts[0].Trim().Select(c => new Card(c)).ToArray();
            
            Debug.Assert(Cards.Length == 5);

            Strength = GetStrength(this);
        }

        public static int GetStrength(Hand h)
        {
            var map = new Dictionary<int, int>();

            foreach (var card in h.Cards)
            {
                if (!map.ContainsKey(card.Value))
                    map.Add(card.Value, 0);

                map[card.Value]++;
            }
                
            if (map.Values.Any(v => v == 5))
                return 6;

            if (map.Values.Any(v => v == 4))
                return 5;

            if (map.Values.Any(v => v == 3))
                return map.Values.Any(v => v == 2) ? 4 : 3;

            if (map.Values.Any(v => v == 2))
                return map.Values.Count(v => v == 2) == 2 ? 2 : 1;

            return 0;
        }
    }
}
