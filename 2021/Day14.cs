using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal class Day14 : Day
    {
        public override string Name => "Day 14: Extended Polymerization";

        private readonly string _template;
        private readonly Dictionary<string, string> _insertionPair = new();
        
        public Day14()
        {
            var input = this.InputString();
            _template = input.First();

            foreach (var line in input.Skip(2))
            {
                var data = line.Split(" -> ");
                _insertionPair.Add(data[0], data[1]);
            }
        }

        public void GrowPolymer(int steps)
        {
            var stage = new List<string> { _template };

            for (var step = 0; step < steps; step++)
            {
                var baseTemplate = stage.Last();
                var builder = new StringBuilder();

                for (var character = 0; character < baseTemplate.Length-1; character++)
                {
                    builder.Append(baseTemplate[character]);
                    
                    var insertion = _insertionPair[$"{baseTemplate[character]}{baseTemplate[character + 1]}"];
                    builder.Append(insertion);
                }
                
                builder.Append(baseTemplate.Last());
                stage.Add(builder.ToString());

                Console.WriteLine(stage.Last());
            }

            if (steps == 10)
            {
                var group = stage.Last().Distinct()
                    .ToDictionary(letter => letter, letter => stage.Last().Count(character => character == letter));
                var mostCommon = group.Max(c => c.Value);
                var leastCommon = group.Min(c => c.Value);
                Console.WriteLine($"Most common - least common = { mostCommon - leastCommon }");
                return;
            }
        }

        public override void FirstAnswer()
        {
            GrowPolymer(10);
        }

        public override void SecondAnswer()
        {
            //GrowPolymer(100);
        }
    }
}
