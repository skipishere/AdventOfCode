using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal record Day17 : Day
    {
        public override string Name => "Day 17: Trick Shot";

        private readonly int targetXStart;
        private readonly int targetYStart;
        private readonly int targetXEnd;
        private readonly int targetYEnd;

        private int highestY = int.MinValue;
        private int hitsTarget = 0;

        public Day17()
        {
            var match = Regex.Match(this.InputString().First(),
                @"target area: x=(?<xStart>-?\d+)..(?<xEnd>-?\d+), y=(?<yEnd>-?\d+)..(?<yStart>-?\d+)");

            var getValue = new Func<string, int>((name) => int.Parse(match.Groups[name].Value));

            targetXStart =  getValue("xStart");
            targetYStart =  getValue("yStart");
            targetXEnd =  getValue("xEnd");
            targetYEnd =  getValue("yEnd");

            FireProbes();
        }

        private void FireProbes()
        {
            for (int x = 1; x <= targetXEnd; x++)
            {
                for (int y = targetYEnd; y < 1000; y++)
                {
                    var probe = new Probe(x, y);
                    while (probe.XPosition < targetXEnd && probe.YPosition > targetYEnd)
                    {
                        probe.Step();

                        if (probe.XPosition >= targetXStart 
                            && probe.XPosition <= targetXEnd 
                            && probe.YPosition <= targetYStart 
                            && probe.YPosition >= targetYEnd)
                        {
                            highestY = Math.Max(highestY, probe.HighestY);
                            hitsTarget++;
                            break;
                        }
                    }
                }
            }
        }
        
        public override void FirstAnswer()
        {
            Console.WriteLine($"Highest Y: {highestY}");
        }

        public override void SecondAnswer()
        {
            Console.WriteLine($"Target hit: {hitsTarget}");
        }

        public class Probe
        {
            public int XVelocity { get; private set; }
            public int YVelocity { get; private set; }

            public int XPosition { get; private set; }
            public int YPosition { get; private set; }

            public int HighestY { get; private set; }

            public Probe(int xVelocity, int yVelocity)
            {
                XVelocity = xVelocity;
                YVelocity = yVelocity;
            }

            public void Step()
            {
                XPosition += XVelocity;
                YPosition += YVelocity;

                HighestY = Math.Max(HighestY, YPosition);

                if (XVelocity > 0)
                {
                    XVelocity--;
                }
                else if (XVelocity < 0)
                {
                    XVelocity++;
                }

                YVelocity--;
            }
        }
    }
}
