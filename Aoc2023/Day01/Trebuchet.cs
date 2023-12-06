using System.Text.RegularExpressions;

namespace Aoc2023.Day01
{
    internal class Trebuchet: IProblem
    {
        private readonly string[] input;

        private static Dictionary<string, int> mapping = new()
            {
                { "one", 1 }, 
                { "two", 2 },
                { "three", 3 }, 
                { "four", 4 }, 
                { "five", 5 }, 
                { "six", 6 }, 
                { "seven", 7 }, 
                { "eight", 8 },
                { "nine", 9 }
            };


        public int Day => 1;
        public string Name => "Trebuchet?";

        public Trebuchet()
        {
            
            input = File.ReadAllLines("Day01/input.txt");
        }
        
        public string SolvePart1()
        {
            var sum = input
                .Select(s => char.GetNumericValue(s.First(char.IsDigit)) * 10 + char.GetNumericValue(s.Last(char.IsDigit)))
            .Sum();

            return sum.ToString();
        }

        public string SolvePart2()
        {
            var pattern = @$"(?=(?<digit>\d|{string.Join("|", mapping.Keys)}))";
            var regex = new Regex(pattern, RegexOptions.Compiled);

            double Parse(Match m)
            {
                var s = m.Groups["digit"].Value;
                return char.IsDigit(s.First()) ? char.GetNumericValue(s.First()) : mapping[s];
            }

            return input
                .Select(s =>
                {
                    var matches = regex.Matches(s);
                    
                    return (Parse(matches.First()) * 10) + Parse(matches.Last());
                })
                .Sum()
                .ToString();
        }
    }
}
