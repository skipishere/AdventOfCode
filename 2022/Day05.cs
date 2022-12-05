using System.Text.RegularExpressions;

namespace AdventOfCode2022;

internal partial record Day05 : Day
{
    public override string Name => "Day 5: Camp Cleanup";

    [GeneratedRegex("move (?<quantity>\\d+) from (?<from>\\d) to (?<to>\\d)")]
    private static partial Regex InstructionParse();

    private List<Stack<char>> _containers = new();
    private List<Instruction> _instructions = new();

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
        return "";// Move();
    }

    public override object SecondAnswer()
    {        
        return MoveCrateMover9001();
    }

    private string Move()
    {
        // not a real clone agh!
        var localContainer = new List<Stack<char>>(_containers);
        
        foreach (var instruction in _instructions)
        {
            for (int i = 0; i < instruction.Quantity; i++)
            {
                localContainer[instruction.To-1].Push(localContainer[instruction.From-1].Pop());
            }
        }

        return string.Join("", localContainer.Select(c => c.Peek()));
    }

    private string MoveCrateMover9001()
    {
        // not a real clone agh!
        var localContainer = new List<Stack<char>>(_containers);

        foreach (var instruction in _instructions)
        {
            var stash = new Stack<char>();
            for (int i = 0; i < instruction.Quantity; i++)
            {
                stash.Push(localContainer[instruction.From - 1].Pop());
            }
            while (stash.Any())
            {
                localContainer[instruction.To - 1].Push(stash.Pop());
            }
        }

        return string.Join("", localContainer.Select(c => c.Peek()));
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
            From = GetAndParse("from");
            To = GetAndParse("to");
        }
    }
}
