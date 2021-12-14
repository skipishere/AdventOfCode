namespace AdventOfCode2021
{
    internal class Day14 : Day
    {
        public override string Name => "Day 14: Extended Polymerization";

        private readonly char _lastCharacter;
        private readonly Dictionary<string, long> _template = new();
        private readonly Dictionary<string, string> _insertionPair = new();

        public Day14()
        {
            var input = this.InputString();
            var template = input.First();

            for (int element = 0; element < template.Length - 1; element++)
            {
                _template.CreateAndIncrease($"{template[element]}{template[element + 1]}", 1);
            }
            
            _lastCharacter = template.Last();
            
            foreach (var line in input.Skip(2))
            {
                var data = line.Split(" -> ");
                _insertionPair.Add(data[0], data[1]);
            }
        }

        public long GrowPolymer(int steps)
        {
            var template = new Dictionary<string, long> (_template);

            for (var step = 0; step < steps; step++)
            {
                var output = new Dictionary<string, long>();
                foreach (var element in template)
                {
                    output.CreateAndIncrease(element.Key.First() + _insertionPair[element.Key], element.Value);
                    output.CreateAndIncrease(_insertionPair[element.Key] + element.Key.Last(), element.Value);
                }

                template = new Dictionary<string, long>(output);
            }

            var letters = new Dictionary<char, long>();
            foreach (var element in template)
            {
                letters.CreateAndIncrease(element.Key.First(), element.Value);
            }

            // As the last character won't be added using the above...
            letters[_lastCharacter] += 1;

            return letters.Max(c => c.Value) - letters.Min(c => c.Value);
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(GrowPolymer(10));
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(GrowPolymer(40));
        }
    }

    public static class Extension
    {
        public static void CreateAndIncrease<TKey>(this Dictionary<TKey, long> dictionary,
            TKey key,
            long value)
            where TKey : notnull
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
