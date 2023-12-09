using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day08
{
    internal class HauntedWasteland: IProblem
    {
        public string Name => "Haunted Wasteland";
        public int Day => 8;

        private readonly string instructions;

        private static Regex regex = new Regex(
            "(?<node>([A-Z1-9]{3})) = \\((?<left>([A-Z1-9]{3})), (?<right>([A-Z1-9]{3}))\\)",
            RegexOptions.Compiled
        );

        private readonly Dictionary<string, Node> map = new();

        public HauntedWasteland()
        {
            var input = File.ReadAllLines("Day08/input.txt");

            instructions = input.First();

            foreach (var line in input.Skip(2))
            {
                var match = regex.Matches(line).First();
                var name = match.Groups["node"].Value;

                var node = new Node(name);

                map.Add(node.Name, node);
            }

            foreach (var line in input.Skip(2))
            {
                var match = regex.Matches(line).First();

                var name = match.Groups["node"].Value;
                var node = map[name];

                var left = match.Groups["left"].Value;
                var right = match.Groups["right"].Value;

                node.Left = map[left];
                node.Right = map[right];
            }
        }

        public string SolvePart1()
        {
            var steps = 0;
            var current = map["AAA"];

            while (current.Name != "ZZZ")
            {
                foreach (var instruction in instructions)
                {
                    steps++;

                    current = instruction == 'L' ? current.Left : current.Right;
                    
                    if (current.Name == "ZZZ")
                        break;
                }
            }

            return steps.ToString();
        }

        public string SolvePart2()
        {
            var history = map.Where(e => e.Key.Last() == 'A').Select(e => new History(e.Value)).ToList();
            
            foreach (var entry in history)
            {
                var finished = false;

                while (!finished)
                {
                    foreach (var instruction in instructions)
                    {
                        entry.Step();
                        entry.Current = instruction == 'L' ? entry.Current.Left : entry.Current.Right;

                        if (entry.Current.Name.Last() == 'Z')
                        {
                            if (entry.Found == null)
                            {
                                entry.Found = entry.Current;
                            }
                            else
                            {
                                finished = true;

                                break;
                            }
                        }
                    }
                }
            }

            long memory = history[0].StepsFound;

            foreach (var entry in history.Skip(1))
            {
                memory = GetLeastCommonMultiple(memory, entry.StepsFound);
            }

            return memory.ToString();
        }

        static long GetGreatestCommonDivider(long a, long b)
        {
             while (b != 0)
            {
                long t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        static long GetLeastCommonMultiple(long a, long b)
        {
            return (a / GetGreatestCommonDivider(a, b)) * b;
        }
    }
}
