using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023.Day08
{
    internal class Node
    {
        public string Name { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
