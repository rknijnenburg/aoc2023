using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day10
{
    internal class Tile
    {
        public char C { get; }
        public int X { get; }
        public int Y { get; }
        public bool IsLoop { get; set; }
        public IEnumerable<Direction> Connections { get; }

        public Tile(char c, int x, int y, IEnumerable<Direction> connections)
        {
            C = c;
            X = x;
            Y = y;
            Connections = connections;
        }

        public override string ToString()
        {
            return $"{X},{Y}: {C}";
        }
    }

}
