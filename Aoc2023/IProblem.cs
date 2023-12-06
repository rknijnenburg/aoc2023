﻿namespace Aoc2023
{
    public interface IProblem
    {
        int Day { get; }
        string Name { get; }

        string SolvePart1();
        string SolvePart2();
    }
}