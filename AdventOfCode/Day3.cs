using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal sealed class Day3
    {
        const char Tree = '#';
                
        private List<string> map;
        int mapLength;
        int mapHeight;

        public void Run()
        {
            map = File.ReadLines(@"input\day3.txt").ToList();

            mapLength = map[0].Length;
            mapHeight = map.Count();

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            long total = 1;
            total *= TreeCount(1, 1);
            total *= TreeCount(3, 1);
            total *= TreeCount(5, 1);
            total *= TreeCount(7, 1);
            total *= TreeCount(1, 2);
            
            Console.WriteLine(total);
            
            timer.Stop();

            Console.WriteLine($"Time taken: {timer.Elapsed}");
        }

        private int TreeCount(int right, int down)
        {
            var treeHit = 0;
            var x = 0;

            for (int i = 0; i < mapHeight; i+=down)
            {
                if (map[i].ElementAt(x % mapLength) == Tree)
                {
                    treeHit++;
                }
                x += right;
            }

            return treeHit;
        }
    }
}
