using System.Text.RegularExpressions;

namespace AdventOfCode2023;

internal record Day01 : Day
{
    public override string Name => "Day 1: Trebuchet?!";

    private readonly IEnumerable<string> _input;

    private readonly Dictionary<string, int> _replaceWords = new()
    {
        { "one", 1 },
        { "two", 2},
        { "three", 3},
        { "four", 4},
        { "five", 5},
        { "six", 6},
        { "seven", 7},
        { "eight", 8},
        { "nine", 9},
    };
    
    public Day01()
    {
        _input = InputString();
    }

    public override object FirstAnswer()
    {
        var cleanInput = _input.Select(
            row => Regex.Replace(row, @"\D", ""));

        return cleanInput.Sum(
            row => 
                int.Parse(row.First().ToString()) * 10 
                + int.Parse(row.Last().ToString()));
    }

    public override object SecondAnswer()
    {
        return _input.Sum(row => FirstNumber(row) * 10 + LastNumber(row));
    }

    private int FirstNumber(string input)
    {
        string segment;
        for (var i = 0; i < input.Length; i++)
        {
            segment = input[..(i+1)];
            if (char.IsDigit(input[i]))
                return int.Parse(input[i].ToString());

            foreach (var key in _replaceWords.Where(key => segment.Contains(key.Key)))
            {
                return key.Value;
            }
        }
        
        throw new Exception("No number found in input");
    }

    private int LastNumber(string input)
    {
        string segment;
        for (var i = input.Length -1; i >= 0; i--)
        {
            segment = input[i..];
            if (char.IsDigit(input[i]))
                return int.Parse(input[i].ToString());

            foreach (var key in _replaceWords.Where(key => segment.Contains(key.Key)))
            {
                return key.Value;
            }
        }
        
        throw new Exception("No number found in input");
    }
}
