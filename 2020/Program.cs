using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day5();
            day.Run();

            Console.ReadLine();
        }
    }
}
