using System.Diagnostics;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Please enter in the day you wish to run, or leave blank for today ({DateTime.Now.Day})");
            
            var input = Console.ReadLine();
            if (!int.TryParse(input, out int result))
            {
                result = DateTime.Now.Day;
            }

            var day = Type.GetType($"AdventOfCode2021.Day{result:D2}");

            if (day != null && Activator.CreateInstance(day) is Day makeMyDay)
            {
                var runWithTimer = new Action<Delegate, string>((puzzle, partNumber) => 
                {
                    var timer = new Stopwatch();
                    Console.WriteLine($"{makeMyDay.Name} Part {partNumber}");
                    timer.Start();
                    puzzle.DynamicInvoke();
                    timer.Stop();
                    Console.WriteLine(@$"Time to solve: {timer.Elapsed:g}");
                    Console.WriteLine(@$"Time to solve: {timer.ElapsedMilliseconds}ms");
                    Console.WriteLine();
                });


                var overall = new Stopwatch();
                overall.Start();
                runWithTimer(() => makeMyDay.FirstAnswer(), "One");                
                runWithTimer(() => makeMyDay.SecondAnswer(), "Two");
                overall.Stop();

                Console.WriteLine(@$"Overall time to solve: {overall.Elapsed:g}");
                Console.WriteLine(@$"Overall time to solve: {overall.ElapsedMilliseconds}ms");
            }
            else 
            {
                Console.WriteLine($"There is no current solution for Day {result}");
            }
        }
    }
}
