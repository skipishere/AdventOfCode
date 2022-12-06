namespace AdventOfCode2022;

internal partial record Day06 : Day
{
    public override string Name => "Day 6: Tuning Trouble";

    private readonly char[] _signal;

    public Day06()
    {
        _signal = InputString().First().ToCharArray();
    }

    public override object FirstAnswer()
    {
        return Unique(4);
    }

    public override object SecondAnswer()
    {
        return Unique(14);
    }

    private int Unique(int uniquelength)
    {
        for (int i = uniquelength-1; i < _signal.Length; i++)
        {
            var block = _signal.Skip(i - uniquelength+1).Take(uniquelength).Distinct().Count();
            if (block == uniquelength)
            {
                return i + 1;
            }
        }

        // Shouldn't happen... really shouldn't 🤣
        return 0;
    }
}
