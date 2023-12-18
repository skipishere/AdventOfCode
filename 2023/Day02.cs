using System.Text.RegularExpressions;

namespace AdventOfCode2023;

internal record Day02 : Day
{
    public override string Name => "Day 2: Cube Conundrum";

    private readonly List<Game> _input = new();

    private record Game
    {
        public int Id { get; init; }
        public int Red { get; init; }
        public int Green { get; init; }
        public int Blue { get; init; }
    }

    public Day02()
    {
        var gameRegex = new Regex(@"Game (?<id>\d+):");
        var redSearch = new Regex(@"(\d+) red");
        var blueSearch = new Regex(@"(\d+) blue");
        var greenSearch = new Regex(@"(\d+) green");
        foreach (var line in InputString())
        {
            var id = int.Parse(gameRegex.Match(line).Groups["id"].Value);
            var part = line.Split(";");

            foreach (var game in part)
            {
                var redText = redSearch.Match(game).Groups[1].Value;
                var greenText = greenSearch.Match(game).Groups[1].Value;
                var blueText = blueSearch.Match(game).Groups[1].Value;
                
                int.TryParse(redText, out var red);
                int.TryParse(greenText, out var green);
                int.TryParse(blueText, out var blue);

                _input.Add(new Game { Id = id, Red = red, Green = green, Blue = blue });
            }
        }
        
    }

    public override object FirstAnswer()
    {
        var maxRed = 12;
        var maxGreen = 13;
        var maxBlue = 14;

        var result = 0;

        _input.GroupBy(c => c.Id).ToList().ForEach(g =>
        {
            var red = g.Max(c => c.Red);
            var green = g.Max(c => c.Green);
            var blue = g.Max(c => c.Blue);

            if (red <= maxRed && green <= maxGreen && blue <= maxBlue)
            {
                result += g.Key;
            }
        });
        
        return result;
    }

    public override object SecondAnswer()
    {
        var result = 0;
        
        _input.GroupBy(c => c.Id).ToList().ForEach(g =>
        {
            var red = g.Max(c => c.Red);
            var green = g.Max(c => c.Green);
            var blue = g.Max(c => c.Blue);

            result += red * green * blue;
        });

        return result;
    }

}
