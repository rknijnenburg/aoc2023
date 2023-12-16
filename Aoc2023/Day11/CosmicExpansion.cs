using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day11
{
    internal class CosmicExpansion: IProblem
    {
        public string Name => "Cosmic Expansion";
        public int Day => 11;

        private readonly List<Point> planets;
        private readonly List<int> ex;
        private readonly List<int> ey;

        public CosmicExpansion()
        {
            var input = File.ReadAllLines("Day11/input.txt");

            planets = new List<Point>();

            for (int y = 0; y < input.Length; y++)
                for (int x = 0; x < input[y].Length; x++)
                    if (input[y][x] == '#')
                        planets.Add(new Point(x, y));

            ex = input
                .Select((s, x) => new { X = x, Line = input.Select(s => s[x]) })
                .Where(p => p.Line.All(c => c == '.'))
                .Select(p => p.X)
                .ToList();

            ey = input
                .Select((s, y) => new { Y = y, Line = s })
                .Where(p => p.Line.All(c => c == '.'))
                .Select(p => p.Y)
                .ToList();
        }

        public string SolvePart1()
        {
            return GetShortestPaths(1).ToString();
        }

        public string SolvePart2()
        {
            return GetShortestPaths(1000000 - 1).ToString();
        }

        private long GetShortestPaths(long expansion)
        {
            var sum = 0L;

            for (int i = 0; i < planets.Count; i++)
            {
                for (int j = i + 1; j < planets.Count; j++)
                {
                    var vx = GetVX(planets[i], planets[j], expansion);
                    var vy = GetVY(planets[i], planets[j], expansion);

                    sum += vx + vy;
                }
            }

            return sum;
        }

        private long GetVX(Point p1, Point p2, long expansion)
        {
            var minx = Math.Min(p1.X, p2.X);
            var maxx = Math.Max(p1.X, p2.X);

            var count = ex.Count(p => p > minx && p < maxx);
            var increase = count * expansion;

            return maxx - minx + increase;
        }

        private long GetVY(Point p1, Point p2, long expansion)
        {
            var miny = Math.Min(p1.Y, p2.Y);
            var maxy = Math.Max(p1.Y, p2.Y);

            var count = ey.Count(p => p > miny && p < maxy);
            var increase = count * expansion;

            return maxy - miny + increase;
        }

    }
}
