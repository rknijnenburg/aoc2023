using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day06
{
    internal class Record
    {
        public long Time { get; }
        public long Distance { get; }

        public Record(long time, long distance)
        {
            Time = time;
            Distance = distance;
        }
    }
}
