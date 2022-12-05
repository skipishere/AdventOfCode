using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal partial record Day05 : Day
{
    public override string Name => "Day 5: Supply Stacks";

    [GeneratedRegex("move (?<quantity>\\d+) from (?<from>\\d) to (?<to>\\d)")]
    private static partial Regex InstructionParse();

    private readonly List<Stack<char>> _containers = new(8);
    private readonly List<Instruction> _instructions = new();

    public Day05()
    {
        var data = InputString().ToList();

        // Initialise
        for (int i = 0; i < 9; i++)
        {
            _containers.Add(new Stack<char>());
        }

        // Load in containers (from bottom to top)
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j < data[i].Length; j+=4)
            {
                var letter = data[i][j+1];
                if (letter != ' ')
                {
                    _containers[j / 4].Push(letter);
                }
            }            
        }

        // Load in instructions
        foreach (var item in data.Skip(10))
        {
            _instructions.Add(new Instruction(item));
        }
    }

    public override object FirstAnswer()
    {
        return Move();
    }

    public override object SecondAnswer()
    {        
        return Move(true);
    }

    private string Move(bool crateMover9001 = false)
    {
        var localContainers = _containers.ConvertAll(s => new Stack<char>(s.Reverse().ToArray()));

        foreach (var instruction in _instructions)
        {
            var stash = new Stack<char>(instruction.Quantity - 1);
            for (int i = 0; i < instruction.Quantity; i++)
            {
                var crate = localContainers[instruction.From].Pop();
                if (crateMover9001)
                {
                    stash.Push(crate);
                }
                else
                {
                    localContainers[instruction.To].Push(crate);
                }
            }

            while (stash.Any())
            {
                localContainers[instruction.To].Push(stash.Pop());
            }
        }

        return string.Join("", localContainers.Select(c => c.Peek()));
    }

    private record Instruction
    {
        public int Quantity { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public Instruction(string input)
        {
            var data = InstructionParse().Match(input);
            int GetAndParse(string group) => int.Parse(data!.Groups[group].Value);

            Quantity = GetAndParse("quantity");
            From = GetAndParse("from") - 1;
            To = GetAndParse("to") - 1;
        }
    }
}
