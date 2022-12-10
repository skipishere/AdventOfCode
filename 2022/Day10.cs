namespace AdventOfCode2022;

internal partial record Day10 : Day
{
    public override string Name => "Day 10: Cathode-Ray Tube";

    private List<int?> _addValue = new();

    public Day10()
    {
        foreach (var instuction in InputString())
        {
            var current = instuction.Split(' ');

            if (current[0] == "noop")
            {
                _addValue.Add(null);
            }
            else
            {
                _addValue.Add(null);
                _addValue.Add(int.Parse(current[1]));
            }
        }
    }

    public override object FirstAnswer()
    {
        var result = 0;
        var x = 1;
        var cycle = 0;

        foreach (var instuction in _addValue) 
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
        return "";
    }

    private record Instruction
    {
        public Instruction(string input)
        {

        }
    }
}
