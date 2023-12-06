using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2023.Day05
{
    internal class IfYouGiveASeedAFertilizer : IProblem
    {
        public string Name => "If You Give A Seed A Fertilizer";
        public int Day => 5;

        private readonly string input;

        private readonly long[] seeds;

        private static string[] sections = new[]
        {
            "seed-to-soil",
            "soil-to-fertilizer",
            "fertilizer-to-water",
            "water-to-light",
            "light-to-temperature",
            "temperature-to-humidity",
            "humidity-to-location"
        };

        private Dictionary<string, Map> map = new();

        public IfYouGiveASeedAFertilizer()
        {
            input = File.ReadAllText("Day05/input.txt");

            var builder = new StringBuilder();

            builder.Append(@"seeds:(?<seeds>.*)(.|\n)*");

            foreach (var section in sections)
                builder.Append($@"{section} map:\n(?<{section.Replace("-", "_")}>(.+\n)*)(.|\n)+");

            var pattern = builder.ToString();
            var matches = new Regex(pattern).Matches(input);
            
            seeds = ParseSeeds(matches.Select(m => m.Groups["seeds"].Value).First());

            foreach (var section in sections)
                map.Add(section, new Map(matches.Select(m => m.Groups[section.Replace("-", "_")].Value).First()));
        }

        private long[] ParseSeeds(string s)
        {
            return s.Trim().Split(" ").Select(s => Convert.ToInt64(s)).ToArray();
        }

        public string SolvePart1()
        {
            var ranges = new List<Range>();


            for (int i = 0; i < seeds.Length; i++)
            {
                var seed = seeds[i];

                ranges.Add(new Range(seed, seed + 1));
            }

            return FindLocations(ranges)
                .Min(e => e.Start)
                .ToString();

        }

        public string SolvePart2()
        {
            var ranges = new List<Range>();
            

            for (int i = 0; i < seeds.Length - 1; i += 2)
            {
                var start = seeds[i];
                var length = seeds[i + 1];

                ranges.Add(new Range(start, start + length));
            }

            return FindLocations(ranges)
                .Min(e => e.Start)
                .ToString();
        }

        private IEnumerable<Range> FindLocations(IEnumerable<Range> ranges)
        {
            var locations = new List<Range>();

            foreach (var range in ranges)
            {
                var current = new Stack<Range>();

                current.Push(range);

                foreach (var section in sections)
                {
                    var next = new Stack<Range>();

                    while (current.TryPop(out var entry))
                    {
                        foreach (var result in map[section].Follow(entry))
                            next.Push(result);
                    }

                    current = next;
                }

                locations.AddRange(current.ToArray());
            }

            return locations;
        }

    }
}
