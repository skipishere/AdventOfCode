using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal record Day22 : Day
    {
        public override string Name => "Day 22: Reactor Reboot";

        private List<Instruction> instructions = new();

        public Day22()
        {
            var input = this.InputString();
            foreach (var line in input)
            {
                instructions.Add(new Instruction(line));
            }
        }

        private long RunInstructions(IEnumerable<Instruction> instructionSet)
        {
            var cubes = new HashSet<Coords>();
            foreach (var instruction in instructionSet)
            {
                
                if (instruction.On)
                {
                    cubes = cubes.Union(instruction.Range).ToHashSet();
                }
                else
                {
                    cubes = cubes.Except(instruction.Range).ToHashSet();
                }
            }

            return cubes.Count;
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(RunInstructions(instructions.Where(i => i.InSubSetRange)));
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(RunInstructions(this.instructions));
        }

        public record Coords
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Z { get; set; }

            public bool InSubsetRange() => X >= -50 && Y >= -50 && Z >= -50 && X <= 50 && Y <= 50 && Z <= 50;
        }
                
        public class Instruction
        {
            public Coords StartRange { get; set; }

            public Coords EndRange { get; set; }

            public bool On { get; set; }

            public bool InSubSetRange => StartRange.InSubsetRange() && EndRange.InSubsetRange();

            public Instruction(string input)
            {
                //on x=10..12,y=10..12,z=10..12
                //off x=-49..-33,y=13..31,z=3..15
                var match = Regex.Match(input, @"(?<action>\w+) x=(?<xStart>-?\d+)..(?<xEnd>-?\d+),y=(?<yStart>-?\d+)..(?<yEnd>-?\d+),z=(?<zStart>-?\d+)..(?<zEnd>-?\d+)");
                On = "on".Equals(match.Groups["action"].Value);

                var getValue = new Func<string, int>((name) => int.Parse(match.Groups[name].Value));
                StartRange = new Coords { X = getValue("xStart"), Y = getValue("yStart"), Z = getValue("zStart") };
                EndRange = new Coords { X = getValue("xEnd"), Y = getValue("yEnd"), Z = getValue("zEnd") };
            }

            public HashSet<Coords> Range
            {
                get
                {
                    var range = new HashSet<Coords>();

                    var xStep = StartRange.X <= EndRange.X ? 1 : -1;
                    var yStep = StartRange.Y <= EndRange.Y ? 1 : -1;
                    var zStep = StartRange.Z <= EndRange.Z ? 1 : -1;

                    for (int x = StartRange.X; x <= EndRange.X; x += xStep)
                    {
                        for (int y = StartRange.Y; y <= EndRange.Y; y += yStep)
                        {
                            for (int z = StartRange.Z; z <= EndRange.Z; z += zStep)
                            {
                                range.Add(new Coords { X = x, Y = y, Z = z });
                            }
                        }
                    }

                    return range;
                }
            }
        }
    }
}
