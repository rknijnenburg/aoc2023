using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace aoc2023.Day02
{
    internal class CubeConundrum : IProblem
    {
        public int Day => 2;
        public string Name => "Cube Conundrum";

        private readonly IEnumerable<Game> games;

        public CubeConundrum()
        {
            var input = File.ReadAllLines("Day02/input.txt");

            games = input.Select(s => new Game(s));
        }

        public string SolvePart1()
        {
            return games
                .Where(g => g.Sets.All(s => s.R <= 12 && s.G <= 13 && s.B <= 14))
                .Sum(g => g.Id)
                .ToString();
        }

        public string SolvePart2()
        {
            return games
                .Select(g => g.Sets.Max(s => s.R) * g.Sets.Max(s => s.G) * g.Sets.Max(s => s.B))
                .Sum()
                .ToString();
        }

    }
}
