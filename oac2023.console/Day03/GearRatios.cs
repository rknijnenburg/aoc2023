using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace aoc2023.Day03
{
    internal class GearRatios : IProblem
    {
        public string Name => "Gear Ratios";
        public int Day => 3;

        private readonly string[] input;
        
        public GearRatios()
        {
            input = File.ReadAllLines("Day03/input.txt");
        }

        private IEnumerable<string> GetAdjacentSymbols(int sx, int sy, int value)
        {
            var length = $"{value}".Length;

            for (int y = sy - 1; y <= sy + 1; y++)
            {
                for (int x = sx - 1; x <= sx + length; x++)
                {
                    if (x >= 0 && y >= 0 && y < input.Length && x < input[y].Length)
                    {
                        var c = input[y][x];

                        if (c != '.' && !char.IsDigit(c))
                            yield return $"{x},{y},{input[y][x]}";
                    }
                }
            }
        }

        public string SolvePart1()
        {
            var regex = new Regex(@"(\d+)", RegexOptions.Compiled);

            return input
                .Select((s, y) =>
                {
                    var sum = 0;

                    foreach (Match match in regex.Matches(s))
                    {
                        var value = Convert.ToInt32(match.Value);

                        if (GetAdjacentSymbols(match.Index, y, value).Any())
                            sum += value;
                    }

                    return sum;
                })
                .Sum()
                .ToString();
        }

        public string SolvePart2()
        {
            var regex = new Regex(@"(\d+)", RegexOptions.Compiled);
            var map = new Dictionary<string, List<int>>();

            for (int y = 0; y < input.Length; y++)
            {
                foreach (Match match in regex.Matches(input[y]))
                {
                    var value = Convert.ToInt32(match.Value);

                    foreach (var key in GetAdjacentSymbols(match.Index, y, value))
                    {
                        if (!map.ContainsKey(key))
                            map.Add(key, new List<int>());

                        map[key].Add(value);
                    }
                }
            }

            return map
                .Values
                .Where(e => e.Count == 2)
                .Select(e => e[0] * e[1])
                .Sum()
                .ToString();
        }
    }
}
