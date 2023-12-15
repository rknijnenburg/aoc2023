namespace Aoc2023.Day10
{
    internal class PipeMaze: IProblem
    {
        public string Name => "Pipe Maze";
        public int Day => 10;

        private readonly string[] input;
        private readonly Tile[][] grid;

        private readonly Tile start;
        private readonly int length;

        public PipeMaze()
        {
            input = File.ReadAllLines("Day10/input.txt");

            grid = new Tile[input.Length][];

            for (var y = 0; y < input.Length; y++)
            {
                grid[y] = new Tile[input[y].Length];

                for (var x = 0; x < input[y].Length; x++)
                {
                    var tile = new Tile(input[y][x], x, y, GetConnections(x, y));

                    if (tile.C == 'S')
                    {
                        start = tile;
                        start.IsLoop = true;
                    }

                    grid[y][x] = tile;
                }
            }

            if (start == null)
                throw new ArgumentNullException();

            var direction = start.Connections.First();
            var current = GetNeighbor(start, direction) ?? throw new ArgumentNullException();

            while (current.X != start.X || current.Y != start.Y)
            {
                length++;

                current.IsLoop = true;

                direction = Next(current, direction);
                current = GetNeighbor(current, direction) ?? throw new ArgumentNullException();
            }
        }

        public string SolvePart1()
        {
            return (length / 2).ToString();
        }

        public string SolvePart2()
        {

            var count = 0;

            for (int y = 0; y < grid.Length; y++)
            {
                var inside = false;
                var last = (Tile?) null;

                for (int x = 0; x < grid[y].Length; x++)
                {
                    var tile = grid[y][x];

                    if (IsTransition(tile, last))
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

        private bool IsTransition(Tile tile, Tile? last)
        {
            if (!tile.IsLoop)
                return false;

            if (last == null)
                return true;

            if (tile.C == '|')
                return true;

            if (tile.C == '-')
                return false;

            if (last.Connections.Contains(Direction.East) && tile.Connections.Contains(Direction.West))
                return last.Connections.Any(l => l is Direction.North or Direction.South && tile.Connections.Contains(l));

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

        private Tile? GetNeighbor(Tile t, Direction d)
        {
            switch (d)
            {
                case Direction.North:
                    return t.Y > 0 ? grid[t.Y - 1][t.X] : null;
                case Direction.East:
                    return t.X + 1 < grid[t.Y].Length ? grid[t.Y][t.X + 1] : null;
                case Direction.South:
                    return t.Y + 1 < grid.Length ? grid[t.Y + 1][t.X] : null;
                case Direction.West:
                    return t.X > 0 ? grid[t.Y][t.X - 1] : null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
        }

        private IEnumerable<Direction> GetConnections(int x, int y)
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
                    if (y > 0 && GetConnections(x, y - 1).Contains(Direction.South))
                        list.Add(Direction.North);
                    if (y + 1 < input.Length && GetConnections(x, y + 1).Contains(Direction.North))
                        list.Add(Direction.South);
                    if (x > 0 && GetConnections(x - 1, y).Contains(Direction.East))
                        list.Add(Direction.West);
                    if (x + 1 < input[y].Length && GetConnections(x + 1, y).Contains(Direction.West))
                        list.Add(Direction.East);
                    return list.ToArray();
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }
    }
}
