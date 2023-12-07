using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day06
{
    internal class WaitForIt: IProblem
    {
        public int Day => 6;
        public string Name => "Wait For It";

        private int[] times;
        private int[] distances;

        public WaitForIt()
        {
            var input = File.ReadAllLines("Day06/input.txt");

            var regex = new Regex(@"(\d+)", RegexOptions.Compiled);

            times = regex.Matches(input[0]).Select(m => Convert.ToInt32(m.Value)).ToArray();
            distances = regex.Matches(input[1]).Select(m => Convert.ToInt32(m.Value)).ToArray();

            
        }

        public string SolvePart1()
        {
            var records = new List<Record>();

            for (var i = 0; i < times.Length; i++)
                records.Add(new Record(times[i], distances[i]));

            var product = 1;

            foreach (var record in records)
                product *= GetFaster(record);

            return product.ToString();
        }

        public string SolvePart2()
        {
            var distance = Convert.ToInt64(String.Join("", distances.Select(e => e.ToString())));
            var time = Convert.ToInt64(String.Join("", times.Select(e => e.ToString())));

            var record = new Record(time, distance);

            return GetFaster(record, 14)
                .ToString();
        }

        private int GetFaster(Record record, int offset = 0)
        {
            var count = 0;

            for (int i = offset; i < record.Time - (offset - 1); i++)
            {
                var distance = i * (record.Time - i);

                if (distance > record.Distance)
                    count++;
            }

            return count;
        }
    }
}
