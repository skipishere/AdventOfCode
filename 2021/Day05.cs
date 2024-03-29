﻿namespace AdventOfCode2021
{
    internal record Day05 : Day
    {
        public override string Name => "Day 5: Hydrothermal Venture";

        private readonly List<Line> _lines = new();

        public Day05()
        {
            var input = this.InputString();

            
            foreach (var data in input)
            {
                _lines.Add(new Line(data));
            }
        }

        public int CalculateDanger(bool includeDiagonal)
        {
            var danger = _lines
                .Where(c => c.Direction != (includeDiagonal ? Line.DirectionType.Unknown: Line.DirectionType.Diagonal))
                .SelectMany(c=> c.GetCoords());

            var mostDanger = danger
                .GroupBy(i => i.ToString())
                .Where(c => c.Count() > 1);

            return mostDanger.Count();
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(CalculateDanger(false));
        }


        public override void SecondAnswer()
        {
            Console.WriteLine(CalculateDanger(true));
        }

        internal class Coords
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Coords(string[] input) : this(int.Parse(input[0]), int.Parse(input[1])) { }

            public Coords(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object? obj)
            {
                if (obj is Coords compare)
                {
                    return (compare.X == X && compare.Y == Y);
                }
                
                return base.Equals(obj);
            }

            public override string ToString()
            {
                return $"{X},{Y}";
            }

            public override int GetHashCode()
            {
                return $"{X},{Y}".GetHashCode();
            }
        }

        internal class Line
        {
            public enum DirectionType
            {
                Unknown,
                Horizontal,
                Vertical,
                Diagonal
            }

            public Coords Start { get; set; }

            public Coords End { get; set; }

            public DirectionType Direction { get; private set; }

            public Line(string input)
            {
                var coords = input.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);

                Start = new Coords(coords[0].Split(","));
                End = new Coords(coords[1].Split(","));
                
                if (Start.X == End.X)
                {
                    Direction = DirectionType.Vertical;
                } else if (Start.Y == End.Y)
                {
                    Direction = DirectionType.Horizontal;
                }
                else
                {
                    Direction = DirectionType.Diagonal;
                }
            }

            public List<Coords> GetCoords()
            {
                var coords = new List<Coords>();

                switch (Direction)
                {
                    case DirectionType.Horizontal:
                        var xStep = (Start.X > End.X) ? -1 : 1;
                        
                        for (int i = Start.X; i != End.X + xStep; i+=xStep)
                        {
                            coords.Add(new Coords(i, Start.Y));
                        }
                        break;

                    case DirectionType.Vertical:
                        var yStep = (Start.Y > End.Y) ? -1 : 1;
                        for (int i = Start.Y; i != End.Y + yStep; i+=yStep)
                        {
                            coords.Add(new Coords(Start.X, i));
                        }
                        break;

                    case DirectionType.Diagonal:
                        var stepDiff = Math.Abs(Start.X - End.X);

                        var yDiagStep = (Start.Y > End.Y) ? -1 : 1;
                        var xDiagStep = (Start.X > End.X) ? -1 : 1;

                        for (int i = 0; i <= stepDiff; i++)
                        {
                            coords.Add(new Coords(Start.X + (i * xDiagStep), Start.Y + (i * yDiagStep)));
                        }
                        break;
                }

                return coords;
            }
        }
    }
}
