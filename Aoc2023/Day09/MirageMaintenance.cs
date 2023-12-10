using System.Xml.Schema;

namespace Aoc2023.Day09
{
    internal class MirageMaintenance: IProblem
    {
        public string Name => "Mirage Maintenance";
        public int Day => 9;

        private readonly string[] input;

        public MirageMaintenance()
        {
            input = File.ReadAllLines("Day09/input.txt");
        }

        public string SolvePart1()
        {
            var sum = 0;

            foreach (var line in input)
            {
                var numbers = line.Split(" ").Select(Int32.Parse).ToList();

                sum += numbers.Last();

                while (numbers.Any(e => e != 0))
                {
                    var result = new List<int>();

                    for (int i = 0; i < numbers.Count - 1; i++)
                    {
                        result.Add(numbers[i + 1] - numbers[i]);
                    }

                    sum += result.Last();

                    numbers = result;
                }
            }

            return sum.ToString();
        }

        public string SolvePart2()
        {
            var total = 0;
            
            foreach (var line in input)
            {
                var numbers = line.Split(" ").Select(int.Parse).ToList();

                var values = new List<int>() { numbers.First() };

                while (numbers.Any(e => e != 0))
                {
                    var result = new List<int>();

                    for (int i = 0; i < numbers.Count - 1; i++)
                        result.Add(numbers[i+1] - numbers[i]);

                    values.Insert(0, result.First());

                    numbers = result;
                }

                var sum = 0;

                foreach (var v in values)
                    sum = v - sum;

                total += sum;
            }

            return total.ToString();
        }
    }
}
