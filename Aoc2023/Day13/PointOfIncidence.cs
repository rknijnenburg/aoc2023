using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Aoc2023.Day13
{
    internal class PointOfIncidence: IProblem
    {
        public string Name => "Point of Incidence";
        public int Day => 13;

        private List<string[]> patterns = new();

        public PointOfIncidence()
        {
            var input = File.ReadAllLines("Day13/input.txt");

            var lines = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    patterns.Add(lines.ToArray());

                    lines.Clear();
                }
                else
                {
                    lines.Add(input[i]);
                }
            }

            if (lines.Any())
                patterns.Add(lines.ToArray());
        }

        public string SolvePart1()
        {
            var sum = 0;

            foreach (var pattern in patterns)
            {
                foreach (var row in FindMirrorRows(pattern))
                    sum += row * 100;

                foreach (var col in FindMirrorColumns(pattern))
                    sum += col;
            }

            return sum.ToString();
        }

        public string SolvePart2()
        {
            var sum = 0;

            foreach (var pattern in patterns)
            {
                var orows = FindMirrorRows(pattern).ToList();
                var ocols = FindMirrorColumns(pattern).ToList();
                var nrows = new List<int>();
                var ncols = new List<int>();

                for (var y = 0; y < pattern.Length; y++)
                {
                    for (var x = 0; x < pattern[y].Length; x++)
                    {
                        var copy = pattern.Clone() as string[];

                        var line = copy[y].ToCharArray();

                        line[x] = line[x] == '.' ? '#' : '.';

                        copy[y] = new string(line);

                        foreach (var row in FindMirrorRows(copy))
                        {
                            if (!orows.Contains(row) && !nrows.Contains(row))
                                nrows.Add(row);
                        }

                        foreach (var col in FindMirrorColumns(copy))
                        {
                            if (!ocols.Contains(col) && !ncols.Contains(col))
                                ncols.Add(col);
                        }
                    }
                }

                sum += (nrows.Sum() * 100) + ncols.Sum();
            }

            return sum.ToString();
        }

        private IEnumerable<int> FindMirrorColumns(string[] pattern)
        {
            var result = new List<int>();

            for (var s = 0; s < pattern[0].Length - 1; s++)
            {
                for (var y = 0; y < pattern.Length; y++)
                {
                    bool xf = true;

                    for (var x = s; x >= 0; x--)
                    {
                        var xl = x ;
                        var xr = s + (s - x + 1);

                        if (xr >= pattern[0].Length)
                            break;

                        var l = pattern[y][xl];
                        var r = pattern[y][xr];

                        if (l != r)
                        {
                            xf = false;
                            break;
                        }
                            
                    }

                    if (!xf)
                        break;

                    if (y == pattern.Length - 1)
                        result.Add(s + 1);
                }
            }

            return result;
        }

        private IEnumerable<int> FindMirrorRows(string[] pattern)
        {
            var result = new List<int>();

            for (var s = 0; s < pattern.Length - 1; s++)
            {
                for (var x = 0; x < pattern[0].Length; x++)
                {
                    bool yf = true;

                    for (var y = s; y >= 0; y--)
                    {
                        var yu = y;
                        var yd = s + (s - y + 1);

                        if (yd >= pattern.Length)
                            break;

                        var u = pattern[yu][x];
                        var d = pattern[yd][x];

                        if (u != d)
                        {
                            yf = false;
                            break;
                        }

                    }

                    if (!yf)
                        break;

                    if (x == pattern[0].Length - 1)
                        result.Add(s + 1);
                }
            }

            return result;
        }
    }
}
