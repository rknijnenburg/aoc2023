using System.Diagnostics;

namespace Aoc2023.Day05
{
    internal class Range
    {
        public long Start { get; }
        public long End { get; }
        public long Length => End - Start;
        
        public Range(long start, long end)
        {
            Debug.Assert(start < end);

            Start = start;
            End = end;
        }
    }
}
