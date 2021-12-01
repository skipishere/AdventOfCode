using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    internal sealed class Day1 : IDay
    {
        public void Run()
        {
            var input = File.ReadLines(@"input\day1.txt");
            var report = new List<int>();
            const int target = 2020;

            report.AddRange(input.Select(c => int.Parse(c)));
            var reportArray = report.ToArray();

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            Console.WriteLine(Answer(reportArray, target));
            timer.Stop();

            Console.WriteLine($"Time taken: {timer.Elapsed}");
        }

        private int Answer(int[] report, int target)
        {
            int first, second, third;

            for (int x = 0; x < report.Length - 2; x++)
            {
                first = report[x];
                for (int y = x + 1; y < report.Length - 1; y++)
                {
                    second = report[y];
                    if (first + second >= target)
                    {
                        continue;
                    }

                    for (int z = y + 1; z < report.Length; z++)
                    {
                        third = report[z];
                        if (first + second + third == target)
                        {
                            return first * second * third;
                        }
                    }
                }
            }

            return -1;
        }
    }
}