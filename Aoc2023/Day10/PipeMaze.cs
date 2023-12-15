using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;

namespace Aoc2023.Day10
{
    internal class PipeMaze: IProblem
    {
        public string Name => "Pipe Maze";
        public int Day => 10;

        private readonly Tile start;

        private readonly Tile[][] grid;
        private readonly List<Tile> tiles = new();
        
        public PipeMaze()
        {
            var input = File.ReadAllLines("Day10/input.txt");

            grid = new Tile[input.Length][];

            for (var y = 0; y < input.Length; y++)
            {
                grid[y] = new Tile[input[y].Length];

                for (var x = 0; x < input[y].Length; x++)
                {
                    var tile = new Tile(input, x, y);

                    if (tile.C == 'S')
                    {
                        start = tile;
                        start.IsLoop = true;
                    }

                    if (x > 0)
                    {
                        var left = grid[y][x - 1];
                        tile.West = left;
                        left.East = tile;
                    }

                    if (y > 0)
                    {
                        var top = grid[y - 1][x];
                        tile.North = top;
                        top.South = tile;
                    }

                    grid[y][x] = tile;
                    tiles.Add(tile);
                }
            }

            

            var direction = this.start.Connections.First();
            var current = this.start.GetNeighbor(direction);
            var steps = 1;

            while (current.X != start.X || current.Y != start.Y)
            {
                steps++;

                current.IsLoop = true;

                direction = Next(current, direction);
                current = current.GetNeighbor(direction);
            }
        }

        public string SolvePart1()
        {
            return (tiles.Count(t => t.IsLoop) / 2).ToString();
        }

        public string SolvePart2()
        {

            var count = 0;

            for (int y = 0; y < grid.Length; y++)
            {
                bool inside = false;
                var last = (Tile?) null;

                for (int x = 0; x < grid[y].Length; x++)
                {
                    var tile = grid[y][x];

                    if (Toggle(tile, last, inside))
                    {
                        inside = !inside;
                        last = tile;
                    }

                    if (inside && !tile.IsLoop)
                        count++;
                }
            }

            return count.ToString();
        }

        private bool Toggle(Tile tile, Tile? last, bool testInside)
        {
            if (!tile.IsLoop)
                return false;

            if (last == null)
                return true;

            var nst = tile.Connections.Where(c => c is Direction.North or Direction.South);
            var ewt = tile.Connections.Where(c => c is Direction.East or Direction.West);

            if (nst.Count() == 2)
                return true;

            if (ewt.Count() == 2)
                return false;

            if (last.Connections.Contains(Direction.East) && tile.Connections.Contains(Direction.West))
            {
                return last.Connections.Any(l => nst.Contains(l));
            }

                //if (last.Connections.Count(c => tile.Connections.Contains(Opposite(c))) == 2)
                //    return false;

                //if (last.Connections.Count(c => tile.Connections.Contains(c)) == 2)
                //    return false;

            return true;
        }

        private Direction Next(Tile t, Direction d)
        {
            return t.Connections.Single(c => Opposite(c) != d);
        }
        
        private Direction Opposite(Direction d)
        {
            return (Direction) (((int)d + 6) % 4);
        }
    }
}
