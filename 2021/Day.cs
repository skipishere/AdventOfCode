using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal abstract class Day
    {
        public abstract string Name { get; }

        public abstract void FirstAnswer();
        public abstract void SecondAnswer();

        internal IEnumerable<string> InputString() => File.ReadLines($@"inputs\{Name}.txt");

        internal IEnumerable<int> InputInt() => this.InputString().Select(c => int.Parse(c));
    }
}
