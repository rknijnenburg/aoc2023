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

        public Card(char c)
        {
            Character = c;
            Value = GetValue(c);
        }

        public static int GetValue(char c)
        {
            if (char.IsDigit(c))
                return c - '0';

            return 10 + characters.IndexOf(c);
        }

        public override string ToString()
        {
            return $"{Character} ({Value})";
        }
    }
}
