using Newtonsoft.Json.Linq;
using System.Text;

namespace AdventOfCode2022;

internal record Day03 : Day
{
    public override string Name => "Day 3: ";

    private List<Bag> _bags = new();
    
    public Day03()
    {
        foreach (var item in InputString())
        {
            _bags.Add(new Bag(item));
        }
    }

    public override object FirstAnswer()
    {
        return _bags.Sum(c => c.Priority());
    }

    public override object SecondAnswer()
    {
        var priority = 0;

        for (int i = 0; i < _bags.Count(); i += 3)
        {
            var common = _bags[i].Contents
                .Intersect(_bags[i+1].Contents)
                .Intersect(_bags[i+2].Contents)
                .FirstOrDefault();

            priority += CalculateValue(common);
        }

        return priority;
    }

    private static int CalculateValue(char character)
    {
        var value = (short)character;
        if (char.IsUpper(character))
        {
            // A = 27, Z = 52
            return value - 64 + 26;
        }

        // a = 1, z = 26
        return value - 96;
    }

    private record Bag
    {
        private readonly char[] _compartmentOne;
        private readonly char[] _compartmentTwo;
        public string Contents { get; private set; }

        public Bag(string input)
        {
            this.Contents = input;
            var split = input.Length / 2;
            _compartmentOne = input[..(split)].ToCharArray();
            _compartmentTwo = input[split..].ToCharArray();
        }

        public int Priority()
        {
            var character = _compartmentOne.Intersect(_compartmentTwo).FirstOrDefault();
            return CalculateValue(character);
        }
    }
}
