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
        public Dictionary<Direction, Tile> Neighbors { get; } = new ();
        public Tile? North
        {
            get => GetNeighbor(Direction.North);
            set => SetNeighbor(Direction.North, value);
        }

        public Tile? South
        {
            get => GetNeighbor(Direction.South);
            set => SetNeighbor(Direction.South, value);
        }

        public Tile? East
        {
            get => GetNeighbor(Direction.East);
            set => SetNeighbor(Direction.East, value);
        }

        public Tile? West
        {
             get => GetNeighbor(Direction.West);
             set => SetNeighbor(Direction.West, value);
        }
    

        public IEnumerable<Direction> Connections { get; }

        public Tile(string[] input, int x, int y)
        {
            C = input[y][x];
            X = x;
            Y = y;

            Connections = GetConnections(input, x, y);
        }

        public Tile? GetNeighbor(Direction d)
        {
            if (Neighbors.ContainsKey(d))
                return Neighbors[d];

            return null;
        }

        private void SetNeighbor(Direction d, Tile? tile)
        {
            if (tile != null)
                Neighbors.Add(d, tile);
            else
                Neighbors.Remove(d);
        }
        
        private static IEnumerable<Direction> GetConnections(string[] input, int x, int y)
        {
            var c = input[y][x];

            switch (c)
            {
                // vertical pipe connecting north and south
                case '|':
                    return new[] { Direction.North, Direction.South };
                // horizontal pipe connecting east and west
                case '-':
                    return new[] { Direction.East, Direction.West };
                // 90 - degree bend connecting north and east
                case 'L':
                    return new[] { Direction.North, Direction.East };
                // 90 - degree bend connecting north and west
                case 'J':
                    return new[] { Direction.North, Direction.West };
                // 90 - degree bend connecting south and west
                case '7':
                    return new[] { Direction.South, Direction.West };
                // 90 - degree bend connecting south and east
                case 'F':
                    return new[] { Direction.South, Direction.East };
                // ground; there is no pipe in this tile
                case '.':
                    return Array.Empty<Direction>();
                case 'S':
                    var list = new List<Direction>();
                    if (y > 0 && GetConnections(input, x, y - 1).Contains(Direction.South))
                        list.Add(Direction.North);
                    if (y + 1 < input.Length && GetConnections(input, x, y + 1).Contains(Direction.North))
                        list.Add(Direction.South);
                    if (x > 0 && GetConnections(input, x - 1, y).Contains(Direction.East))
                        list.Add(Direction.West);
                    if (x + 1 < input[y].Length && GetConnections(input, x + 1, y).Contains(Direction.West))
                        list.Add(Direction.East);
                    return list.ToArray();
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }

        public override string ToString()
        {
            return $"{X},{Y}: {C}";
        }
    }

}
