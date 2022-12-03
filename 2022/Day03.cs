namespace AdventOfCode2022;

internal record Day03 : Day
{
    public override string Name => "Day 3: ";

    private readonly List<string> _bags;
    
    public Day03()
    {
        _bags = InputString().ToList();
    }

    public override object FirstAnswer()
    {
        var priority = 0;
        foreach (var contents in _bags)
        {
            var split = contents.Length / 2;
            
            var character = contents[..split]
                .Intersect(contents[split..])
                .FirstOrDefault();

            priority += CalculateValue(character);
        }

        return priority;
    }

    public override object SecondAnswer()
    {
        var priority = 0;
        for (var i = 0; i < _bags.Count; i += 3)
        {
            var common = _bags[i]
                .Intersect(_bags[i + 1])
                .Intersect(_bags[i + 2])
                .FirstOrDefault();

            priority += CalculateValue(common);
        }

        return priority;
    }

    private static int CalculateValue(char character)
    {
        var value = character - 38;

        return char.IsUpper(character) 
            ? value  // A = 27, Z = 52
            : value - 58; // a = 1, z = 26
    }
}
