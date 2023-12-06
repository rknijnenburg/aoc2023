using System.Text.RegularExpressions;

namespace Aoc2023.Day04
{
    internal class Scratchcards: IProblem
    {
        public string Name => "Scratchcards";
        public int Day => 4;
        
        private readonly string[] input;

        public Scratchcards()
        {
            input = File.ReadAllLines("Day04/input.txt");
        }

        public string SolvePart1()
        {
            var regex = new Regex(".{3}", RegexOptions.Compiled);

            return input
                .Select(s =>
                {
                    var slices = s.Split(":")[1].Split("|");
                    var winning = regex.Matches(slices[0]).Select(m => m.Value);
                    var amount = winning.Count(w => slices[1].Contains(w));
                    
                    return amount > 0 ? Math.Pow(2, amount - 1) : 0;
                })
                .Sum()
                .ToString();
        }

        public string SolvePart2()
        {
            var regex = new Regex(".{3}", RegexOptions.Compiled);
            var map = new Dictionary<int, int>();

            for (int i = 0; i < input.Length; i++)
            {
                if (!map.ContainsKey(i))
                    map.Add(i, 1);

                var slices = input[i].Split(":")[1].Split("|");
                var winning = regex.Matches(slices[0]).Select(m => m.Value);
                var amount = winning.Count(w => slices[1].Contains(w));

                for (int j = i + 1; j <= i + amount; j++)
                {
                    if (!map.ContainsKey(j))
                        map.Add(j, 1);

                    map[j] += map[i];
                }
            }


            return map
                .Values
                .Sum()
                .ToString();
        }
    }
}
