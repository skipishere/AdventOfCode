using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2022;

internal partial record Day10 : Day
{
    public override string Name => "Day 10: Cathode-Ray Tube";

    private readonly List<int?> _instruction = new();

    public Day10()
    {
        foreach (var input in InputString())
        {
            var current = input.Split(' ');

            if (current[0] == "noop")
            {
                _instruction.Add(null);
            }
            else
            {
                _instruction.Add(null);
                _instruction.Add(int.Parse(current[1]));
            }
        }
    }

    public override object FirstAnswer()
    {
        var result = 0;
        var x = 1;
        var cycle = 0;

        foreach (var instuction in _instruction) 
        {
            cycle++;
            if ((cycle + 20) % 40 == 0)
            {
                Console.WriteLine($"Cycle {cycle} - Value {cycle * x}");
                result += cycle * x;
            }

            if (instuction.HasValue)
            {
                x += instuction.Value;
            }
        }
        
        return result;
    }

    public override object SecondAnswer()
    {
        var x = 1;
        var currentPosition = 0;

        foreach (var instuction in _instruction)
        {
            if (currentPosition == 40)
            {
                currentPosition = 0;
                Console.WriteLine();
            }

            if (currentPosition >= x - 1 && currentPosition <= x + 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("▓");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(".");
            }

            if (instuction.HasValue)
            {
                x += instuction.Value;
            }

            currentPosition++;
        }

        return "";
    }
}
