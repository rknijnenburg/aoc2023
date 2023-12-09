using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023.Day08
{
    internal class History
    {
        public int StepsFound { get; set; } = 0;
        public int StepsLoop { get; set; } = 0;
        public Node Start { get; }
        public Node? Found { get; set; } = null;
        public Node Current { get; set; }

        public History(Node current)
        {
            Current = current;
            Start = current;
        }

        public void Step()
        {
            StepsLoop++;

            if (Found == null)
                StepsFound++;
        }
    }
}
