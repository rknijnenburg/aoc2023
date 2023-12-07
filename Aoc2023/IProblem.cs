namespace Aoc2023
{
    public interface IProblem
    {
        string Name { get; }
        int Day { get; }
        string SolvePart1();
        string SolvePart2();
    }
}
