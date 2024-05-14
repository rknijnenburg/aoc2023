using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Aoc2023.Day15
{
    internal class Lens
    {
        public string Label { get; }
        public int FocalLength { get; }
        public Lens(string instruction)
        {
            var values = instruction.Split('=');
            Label = values[0];
            FocalLength = Convert.ToInt16(values[1]);
        }
    }
}
