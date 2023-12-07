using Aoc2023.Day05;
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

        public Hand(string line, bool jacksWild = false)
        {
            var parts = line.Split(" ");

            Bid = Convert.ToInt32(parts[1].Trim());
            Cards = parts[0].Trim().Select(c => new Card(c, jacksWild)).ToArray();
            
            Debug.Assert(Cards.Length == 5);

            Strength = GetStrength(this, jacksWild);
        }

        public static int GetStrength(Hand h, bool jacksWild)
        {
            var map = new Dictionary<int, int>();

            map.Add(1, 0);

            foreach (var card in h.Cards)
            {
                if (!map.ContainsKey(card.Value))
                    map.Add(card.Value, 0);

                map[card.Value]++;
            }
                
            if (map.Any(p => p.Value == 5 || (jacksWild && p.Key != 1 && (p.Value + map[1]) == 5)))
                return 6;

            if (map.Any(p => p.Value == 4 || (jacksWild && p.Key != 1 && (p.Value + map[1]) == 4)))
                return 5;

            var pairs = map.Count(v => v.Value == 2);

            if (map.Any(p => (p.Value == 3 && pairs == 1) || (jacksWild && p.Key != 1 && pairs == 2 && map[1] == 1)))
                return 4;

            if (map.Any(p => p.Value == 3 || (jacksWild && p.Key != 1 && (p.Value + map[1]) == 3)))
                return 3;

            if (pairs == 2)
                return 2;

            if (pairs == 1 || (jacksWild && map[1] == 1))
                return 1;

            return 0;
        }
    }
}
