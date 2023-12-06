using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2023
{
    public interface IProblem
    {
        int Day { get; }
        string Name { get; }

        string SolvePart1();
        string SolvePart2();
    }
}
