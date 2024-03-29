﻿namespace AdventOfCode2021
{
    internal record Day01 : Day
    {
        public override string Name => "Day 1: Sonar Sweep";

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
            
            for (int i = 0; i < depths.Length - 2; i++)
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
