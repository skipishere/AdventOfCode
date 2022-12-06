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

    private int Unique(int length)
    {
        for (int i = length - 1; i < _signal.Length; i++)
        {
            var unique = _signal
                .Skip(i - length + 1)
                .Take(length)
                .Distinct()
                .Count() == length;

            if (unique)
            {
                return i + 1;
            }
        }

        // Shouldn't happen... really shouldn't 🤣
        return 0;
    }
}
