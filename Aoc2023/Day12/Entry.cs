using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day12
{
    internal class Entry
    {
        public string Row { get; }

        public int[] Record { get; }

        public Entry(string s)
        {
            var slices = s.Split(" ");
            Row = slices[0];
            Record = slices[1].Split(",").Select(Int32.Parse).ToArray();
        }
    }
}
