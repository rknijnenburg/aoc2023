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
    internal static class Temp
    {
        public static bool Compare<T>(this T[,] firstArray, T[,] secondArray)
        {
            //from https://github.com/mohammedsouleymane/AdventOfCode/blob/main/AdventOfCode/Utilities/Util.cs
            return firstArray.Rank == secondArray.Rank &&
                   Enumerable.Range(0, firstArray.Rank).All(dimension => firstArray.GetLength(dimension) == secondArray.GetLength(dimension)) &&
                   firstArray.Cast<T>().SequenceEqual(secondArray.Cast<T>());
        }
    }

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
            var result = Tilt(input);
            
            return Score(result).ToString();
        }

        public string SolvePart2()
        {
            var history = new List<string>();
            var result = input;
            
            for (var i = 0; i < 1000000000; i++)
            {
                result = Cycle(result);

                var test = Score(result);

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

        private char[][] Cycle(char[][] platform)
        {
            var result = platform;

            for (var d = 0; d < 4; d++)
            {
                result = Tilt(result);
                result = Rotate(result);
            }
                

            return result;
        }

        private char[][] Rotate(char[][] platform)
        {
            var result = new char[platform[0].Length][];

            for (var y = 0; y < platform.Length; y++)
            {
                for (var x = 0; x < platform[0].Length; x++)
                {
                    if (result[x] == null)
                        result[x] = new char[platform.Length];

                    var ny = x;
                    var nx = result.Length - 1 - y;

                    result[ny][nx] = platform[y][x];
                }
            }


            return result;
        }

        private char[][] Tilt(char[][] platform)
        {
            for (int y = 1; y < platform.Length; y++)
            {
                for (int x = 0; x < platform[y].Length; x++)
                {
                    for (int v = y; v > 0; v--)
                    {
                        if (platform[v][x] == 'O' && platform[v - 1][x] == '.')
                        {
                            platform[v][x] = '.';
                            platform[v - 1][x] = 'O';
                        }
                    }
                }
            }

            return platform;
        }
    }
}
