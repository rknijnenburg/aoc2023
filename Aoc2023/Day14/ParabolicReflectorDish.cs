using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day14
{
    internal class ParabolicReflectorDish: IProblem
    {
        public string Name => "Parabolic Reflector Dish";
        public int Day => 14;

        private char[][] input;

        public ParabolicReflectorDish()
        {
            input = File.ReadAllLines("Day14/input.txt").Select(s => s.ToCharArray()).ToArray();
        }

        public string SolvePart1()
        {
            var result = input.Clone() as char[][];

            TiltNorth(result);
            
            return Score(result).ToString();
        }

        public string SolvePart2()
        {
            var history = new List<string>();
            var result = input.Clone() as char[][];

            for (var i = 0; i < 1000000000; i++)
            {
                Cycle(result);

                var key = ToString(result);

                if (history.Contains(key))
                {
                    var hindex = history.IndexOf(key);
                    var sindex = ((1000000000 - hindex) % (history.Count - hindex)) + hindex - 1;
                    var platform = ToArray(history[sindex]);

                    return Score(platform).ToString();
                }

                history.Add(key);
            }

            return "NA";
        }
        
        private string ToString(char[][] platform)
        {
            return string.Join(Environment.NewLine, platform.Select(c => string.Join("", c)));
        }

        private char[][] ToArray(string s)
        {
            return s.Split(Environment.NewLine).Select(l => l.ToCharArray()).ToArray();
        }

        private int Score(char[][] platform)
        {
            var sum = 0;

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (platform[y][x] == 'O')
                        sum += (input.Length - y);
                }
            }

            return sum;
        }

        private void Cycle(char[][] platform)
        {
            foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
                Tilt(platform, direction);
        }

        private void Tilt(char[][] platform, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    TiltNorth(platform);
                    break;
                case Direction.West:
                    TiltWest(platform);
                    break;
                case Direction.South:
                    TiltSouth(platform);
                    break;
                case Direction.East:
                    TiltEast(platform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TiltNorth(char[][] platform)
        {
            for (int y = 1; y < platform.Length; y++)
            {
                for (int x = 0; x < platform[y].Length; x++)
                {
                    for (int v = y; v > 0; v--)
                    {
                        if (!IsSwap(platform, v, x, v - 1, x))
                            break;

                        Swap(platform, v, x, v - 1, x);
                    }
                }
            }
        }

        private void TiltSouth(char[][] platform)
        {
            for (int y = platform.Length - 2; y >= 0; y--)
            {
                for (int x = 0; x < platform[y].Length; x++)
                {
                    for (int v = y; v < platform.Length - 1; v++)
                    {
                        if (!IsSwap(platform, v, x, v + 1, x))
                            break;

                        Swap(platform, v, x, v + 1, x);
                    }
                }
            }
        }

        private void TiltWest(char[][] platform)
        {
            for (int y = 0; y < platform.Length; y++)
            {
                for (int x = 1; x < platform[y].Length; x++)
                {
                    for (int v = x; v > 0; v--)
                    {
                        if (!IsSwap(platform, y, v, y, v - 1))
                            break;

                        Swap(platform, y, v, y, v - 1);
                    }
                }
            }
        }

        private void TiltEast(char[][] platform)
        {
            for (int y = 0; y < platform.Length; y++)
            {
                for (int x = platform[y].Length - 2; x >= 0; x--)
                {
                    for (int v = x; v < platform[y].Length - 1; v++)
                    {
                        if (!IsSwap(platform, y, v, y, v + 1))
                            break;

                        Swap(platform, y, v, y, v + 1);
                    }
                }
            }
        }

        private bool IsSwap(char[][] platform, int sy, int sx, int dy, int dx)
        {
            return platform[sy][sx] == 'O' && platform[dy][dx] == '.';
        }

        private void Swap(char[][] platform, int sy, int sx, int dy, int dx)
        {
            platform[sy][sx] = '.';
            platform[dy][dx] = 'O';
        }
    }
}