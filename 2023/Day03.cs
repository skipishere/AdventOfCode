using System.Text.RegularExpressions;

namespace AdventOfCode2023;

internal record Day03 : Day
{
    public override string Name => "Day 03: Gear Ratios";

    private readonly int _length;
    private readonly int _height;
    private readonly Grid[] _schematic;
    
    public Day03()
    {
        var input = InputString().ToArray();

        _schematic = input.SelectMany(c => c.ToCharArray()).Select(d => new Grid { Symbol = d }).ToArray();
        _length = input.First().Length;
        _height = input.Length;
        
        
    }

    public override object FirstAnswer()
    {
        var value = 0;
        for (var i = 0; i < _schematic.Length; i++)
        {
            if (_schematic[i].IsSymbol)
            {
                // TODO work out and skip the next few characters.
                //value += ;
            }
        }
        return null;
    }

    public override object SecondAnswer()
    {
        return null;
    }

    private bool ValidNumeric(int position)
    {
     // 140 x 140   
        return false;
    }

    private record Grid
    {
        public bool IsSymbol => Symbol != '.' && !char.IsDigit(Symbol);
        public char Symbol { get; init; }
    }
}
