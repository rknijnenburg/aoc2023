using Aoc2023.Day05;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day15
{
    internal class LensLibrary: IProblem
    {
        public string Name => "Lens Library";
        public int Day => 15;

        private string[] input;

        public LensLibrary()
        {
            input = File.ReadAllLines("Day15/input.txt")[0].Split(',');
        }

        public string SolvePart1()
        {
            return  input.Sum(Hash).ToString();
        }

        public string SolvePart2()
        {
            var map = Enumerable.Range(0, 256).Select(i => new List<Lens>()).ToArray();
            
            foreach (var instruction in input)
                map = Execute(instruction, map);
            
            return map
                .SelectMany((e, bi) => e.Select((l, si) => (bi + 1) * (si + 1) * l.FocalLength))
                .Sum()
                .ToString();
        }

        private int Hash(string s)
        {
            var result = 0;
            var values = Encoding.ASCII.GetBytes(s);

            foreach (var v in values)
                result = (result + Convert.ToInt16(v)) * 17 % 256;

            return result;
        }

        private List<Lens>[] Execute(string instruction, List<Lens>[] map)
        {
            if (instruction.Contains('='))
            {
                var lens = new Lens(instruction);
                var box = Hash(lens.Label);
                var slot = map[box].FindIndex(l => l.Label == lens.Label);
                if (slot >= 0)
                {
                    map[box].RemoveAt(slot);
                    map[box].Insert(slot, lens);
                }
                else
                {
                    map[box].Add(lens);
                }

            }
            else if (instruction.EndsWith('-'))
            {
                var label = instruction[..^1];
                var box = Hash(label);
                map[box].RemoveAll(l => l.Label == label);
            }
            else
            {
                throw new ArgumentException(nameof(instruction));
            }
            
            return map;
        }
    }
}
