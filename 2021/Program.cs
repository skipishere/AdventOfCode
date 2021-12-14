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
                Console.WriteLine($"{makeMyDay.Name} Part One");
                makeMyDay.FirstAnswer();

                Console.WriteLine($"{makeMyDay.Name} Part Two");
                makeMyDay.SecondAnswer();
            }
            else 
            {
                Console.WriteLine($"There is no current solution for Day {result}");
            }

            Console.ReadLine();
        }
    }
}
