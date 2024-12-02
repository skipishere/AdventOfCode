namespace AdventOfCode2022;

internal partial record Day09 : Day
{
    public override string Name => "Day 9: Rope Bridge";

    private List<Instruction> _instructions;

    public Day09()
    {
        _instructions = InputString()
            .Select(i => new Instruction { Direction = i.First(), Steps = int.Parse(i[1..]) })
            .ToList();
    }

    public override object FirstAnswer()
    {

        return "";
    }

    public override object SecondAnswer()
    {
        return "";
    }

    private record Instruction
    {
        public char Direction { get; set; }
        public int Steps { get; set; }
    }
}
