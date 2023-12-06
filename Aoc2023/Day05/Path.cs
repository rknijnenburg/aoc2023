using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace Aoc2023.Day05
{
    internal class Path
    {
        public Range Source { get; }
        public Range Destination{ get; }
        
        public Path(string s)
        {
            var slices = s.Trim().Split(" ");

            var source = Convert.ToInt64(slices[1]);
            var destination = Convert.ToInt64(slices[0]);
            var length = Convert.ToInt64(slices[2]);

            Source = new Range(source, source + length);
            Destination = new Range(destination, destination + length);
        }
    }
}
