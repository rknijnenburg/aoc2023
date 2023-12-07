using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day07
{
    internal class CamelCards: IProblem
    {
        public string Name => "Camel Cards";
        public int Day => 7;

        private readonly string[] input;
        
        public CamelCards()
        {
            input = File.ReadAllLines("Day07/input.txt");
        }

        public string SolvePart1()
        {
            var hands = input.Select(line => new Hand(line)).ToArray();

            Array.Sort(hands, new Sorter());

            return hands
                .Select((h, i) => h.Bid * (i + 1))
                .Sum()
                .ToString();
        }

        public string SolvePart2()
        {
            var hands = input.Select(line => new Hand(line, true)).ToArray();

            Array.Sort(hands, new Sorter());

            return hands
                .Select((h, i) => h.Bid * (i + 1))
                .Sum()
                .ToString();
        }
    }
}
