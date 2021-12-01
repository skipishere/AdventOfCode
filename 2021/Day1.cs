using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day1 : Day
    {
        
        public override string Name => "day1";


        public override void FirstAnswer()
        {
            var depths = this.InputInt();

            var depthIncreases = -1;
            var lastDepth = 0;
            foreach (var depth in depths)
            {
                if (depth > lastDepth)
                {
                    depthIncreases++;
                }

                lastDepth = depth;
            }

            Console.WriteLine(depthIncreases);
        }

        public override void SecondAnswer()
        {
            var depths = this.InputInt().ToArray();

            var depthIncreases = -1;
            var previousWindow = 0;
            
            for (int i = 0; i < depths.Count() - 2; i++)
            {
                var currentWindow = depths[i] + depths[i+1] + depths[i+2];
                if (currentWindow > previousWindow)
                {
                    depthIncreases++;
                }

                previousWindow = currentWindow;
            }

            Console.WriteLine(depthIncreases);
        }
    }
}
