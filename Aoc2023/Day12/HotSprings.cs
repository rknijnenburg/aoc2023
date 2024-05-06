using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day12
{
    internal class HotSprings: IProblem
    {
        public string Name => "HotSprings";
        public int Day => 12;

        private List<Entry> map = new();

        private ConcurrentDictionary<string, long> Cache = new();

        public HotSprings()
        {
            var input = File.ReadAllLines("Day12/input.txt");

            foreach (var line in input)
                map.Add(new Entry(line));
        }

        public string SolvePart1()
        {
            var sum = 0L;

            foreach (var entry in map)
                sum += GetPossiblePermutationsCount(entry.Record, entry.Row);

            return sum.ToString();
        }
        
        public string SolvePart2()
        {
            var sum = 0L;
            
            foreach (var entry in map)
            {
                var record = Enumerable.Repeat(entry.Record, 5).SelectMany(r => r).ToArray();
                var row = string.Join('?', Enumerable.Repeat(entry.Row, 5));
                
                var test = GetPossiblePermutationsCount(record, row);

                sum += test;
            }

            return sum.ToString();
        }

        private string BuildKey(int[] record, string row)
        {
            return $"{string.Join(',', record)} {row}";
        }
        
        private long GetPossiblePermutationsCount(int[] record, string row)
        {
            // end of record
            if (record.Length == 0)
                return row.Contains('#') ? 0 : 1;

            // end of row
            if (row.Length == 0)
                return 0;

            var result = 0L;

            // operational
            if (row[0] == '.' || row[0] == '?')
                result += Cache.GetOrAdd(BuildKey(record, row[1..]), _ => GetPossiblePermutationsCount(record, row[1..]));

            // broken
            if (row[0] == '#' || row[0] == '?')
            {
                if (record[0] > row.Length)
                    return result;

                if (row[..record[0]].Contains('.'))
                    return result;

                if (record[0] == row.Length || row[record[0]] != '#')
                {
                    var start = Math.Min(row.Length, record[0] + 1);

                    result += Cache.GetOrAdd(BuildKey(record[1..], row[start..]), _ => GetPossiblePermutationsCount(record[1..], row[start..]));
                }
            }

            return result;
        }
    }
}





