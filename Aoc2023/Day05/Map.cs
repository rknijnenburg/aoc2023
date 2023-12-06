using System;
using System.Diagnostics;
using System.IO;

namespace Aoc2023.Day05
{
    internal class Map
    {
        private readonly List<Path> paths = new ();

        public Map(string s)
        {
            var slices = s.Split("\n");

            foreach (var slice in slices)
                if (!string.IsNullOrEmpty(slice))
                    paths.Add(new Path(slice));
        }

        public IEnumerable<Range> Follow(Range range)
        {
            var result = new List<Range>();
            var remaining = new Stack<Range>();

            remaining.Push(range);
            
            while (remaining.TryPop(out var entry))
            {
                Range? processed = null;

                foreach (var path in paths)
                {
                    if (entry.Start < path.Source.End && entry.End > path.Source.Start)
                    {
                        var start = Math.Max(entry.Start, path.Source.Start);
                        var end = Math.Min(entry.End, path.Source.End);
                        var length = end - start;
                        var destination = start + (path.Destination.Start - path.Source.Start);

                        if (entry.Start < start)
                            remaining.Push(new Range(entry.Start, start - 1));

                        if (entry.End > path.Source.End)
                            remaining.Push(new Range(path.Source.End, entry.End));

                        processed = new Range(destination, destination + length);

                        break;
                    }
                }
                
                result.Add(processed ?? entry);
            }

            return result;
        }

        
    }
}
