using System.Diagnostics;

namespace Aoc2023
{
    public partial class Program
    {
        private static readonly Stopwatch stopwatch = Stopwatch.StartNew();

        private static void Solve<TProblem>()
            where TProblem: IProblem
        {
            stopwatch.Restart();

            var problem = Activator.CreateInstance<TProblem>();

            Console.Out.Write($"|{problem.Name,32}");
            Console.Out.Write($"|{problem.Day,4}");
            Console.Out.Write($"|{stopwatch.Elapsed,12:mm':'ss':'ffff}");

            stopwatch.Restart();

            Console.Out.Write($"|{problem.SolvePart1(),20}");
            Console.Out.Write($"|{stopwatch.Elapsed,12:mm':'ss':'ffff}");

            stopwatch.Restart();

            Console.Out.Write($"|{problem.SolvePart2(),20}");
            Console.Out.Write($"|{stopwatch.Elapsed,12:mm':'ss':'ffff}");
            Console.Out.Write("|");
            Console.Out.WriteLine();
        }

        public static void Main(string[] args)
        {
            Console.Out.Write($"|{"Name",32}");
            Console.Out.Write($"|{"Day",4}");
            Console.Out.Write($"|{"Init",12}");
            Console.Out.Write($"|{"Part 1",20}");
            Console.Out.Write($"|{"Elapsed",12}");
            Console.Out.Write($"|{"Part 2",20}");
            Console.Out.Write($"|{"Elapsed",12}");
            Console.Out.Write("|");
            Console.Out.WriteLine();
            Console.Out.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            Solve<Day01.Trebuchet>();
            Solve<Day02.CubeConundrum>();
            Solve<Day03.GearRatios>();
            Solve<Day04.Scratchcards>();
            Solve<Day05.IfYouGiveASeedAFertilizer>();
            Solve<Day06.WaitForIt>();
            Solve<Day07.CamelCards>();
            Solve<Day08.HauntedWasteland>();
            Solve<Day09.MirageMaintenance>();

            Console.ReadKey();
        }
    }
}