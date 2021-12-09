using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO take the day as input, or better yet redo this section...
            Day day = new Day8();

            Console.WriteLine($"{ day.Name} Part One");
            day.FirstAnswer();

            Console.WriteLine($"{ day.Name} Part Two");
            day.SecondAnswer();

            Console.ReadLine();
        }
    }
}
