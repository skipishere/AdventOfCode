using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal class Day13 : Day
    {
        public override string Name => "Day 13: Transparent Origami";

        private List<CoOrds> _dots = new();
        private List<CoOrds> _folds = new();
        
        public Day13()
        {
            foreach (var line in this.InputString())
            {
                if (line.StartsWith("fold"))
                {
                    // add fold

                    var match = Regex.Match(line, @"fold along (?<type>\w)=(?<value>\d*)");
                    var value = int.Parse(match.Groups["value"].Value);
                    if ("x".Equals(match.Groups["type"].Value))
                    {
                        _folds.Add(new CoOrds { X = value, Y = 0 });
                    }
                    else
                    {
                        _folds.Add(new CoOrds { X = 0, Y = value });
                    }
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    var splitCoords = line.Split(',');
                    _dots.Add(new CoOrds { X = int.Parse(splitCoords[0]), Y = int.Parse(splitCoords[1]) });
                }
            }

        }

        public void Fold(int folds)
        {
            var localDots =  new List<CoOrds>(_dots);

            for (int fold = 0; fold < folds; fold++)
            {
                var foldLine = _folds.ElementAt(fold);
                foreach (var dot in localDots)
                {
                    if (foldLine.X > 0 && dot.X > foldLine.X)
                    {
                        // fold along x
                        var diff = dot.X - foldLine.X;
                        dot.X = foldLine.X - diff;
                    }
                    else if (foldLine.Y > 0 && dot.Y > foldLine.Y)
                    {
                        // fold along y
                        var diff = dot.Y - foldLine.Y;
                        dot.Y = foldLine.Y - diff;
                    }
                }

            }
            

            if (folds == 1)
            {
                Console.WriteLine($"Distinct dots = { localDots.Distinct().Count()}");
                return;
            }
            
            for (int y = 0; y <= localDots.Max(d => d.Y); y++)
            {
                for (int x = 0; x <= localDots.Max(d => d.X); x++)
                {
                    if (localDots.Distinct().Contains(new CoOrds { X = x, Y = y }))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('#');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write('.');
                    }
                }

                Console.WriteLine();
            }
            
            Console.ResetColor();
        }

        public override void FirstAnswer()
        {
            Fold(1);
        }

        public override void SecondAnswer()
        {
            Fold(_folds.Count);
        }


        private record class CoOrds
        {
            public int X { get; set; }

            public int Y { get; set; }
        }
    }
}
