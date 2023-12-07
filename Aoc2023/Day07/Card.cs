using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day07
{
    internal class Card
    {
        private static string characters = "TJQKA";

        public char Character { get; }
        public int Value { get; set; }

        public Card(char c, bool jacksWild)
        {
            Character = c;
            Value = GetValue(c, jacksWild);
        }

        public static int GetValue(char c, bool jacksWild)
        {
            if (char.IsDigit(c))
                return c - '0';

            if (jacksWild && c == 'J')
                return 1;

            return 10 + characters.IndexOf(c);
        }

        public override string ToString()
        {
            return $"{Character} ({Value})";
        }
    }
}
