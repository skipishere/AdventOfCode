namespace AdventOfCode2021
{
    internal class Day8 : Day
    {
        public override string Name => "Day 8: Seven Segment Search";

        private IEnumerable<Display> _display = new List<Display>();

        public Day8()
        {
            _display = this.InputString().Select(c => { return new Display(c); });
        }

        public override void FirstAnswer()
        {
            var count = _display.Sum(c => c.Digit.Where(x => x.Length is 2 or 3 or 4 or 7).Count());
            
            Console.WriteLine(count);
        }


        public override void SecondAnswer()
        {
            Console.WriteLine(_display.Sum(c=> c.Value));
        }
    }

    public class Display
    {
        public string[] Signal { get; set; }

        public string[] Digit { get; set; }

        public int Value { get; private set; }

        public Display(string input)
        {
            var split = input.Split(" | ");
            Signal = split[0].Split(' ');
            Digit = split[1].Split(' ');

            Decode();
        }

        private void Decode()
        {
            var decoder = new Dictionary<string, int>();
            
            var one = Signal.Single(c => c.Length == 2).ToCharArray();
            var four = Signal.Single(c => c.Length == 4).ToCharArray();
            var seven = Signal.Single(c => c.Length == 3).ToCharArray();
            var eight = Signal.Single(c => c.Length == 7).ToCharArray();
            char bottomLeft = '\0';

            decoder.Add(Key(one), 1);
            decoder.Add(Key(four), 4);
            decoder.Add(Key(seven), 7);
            decoder.Add(Key(eight), 8);

            var zeroSixNine = Signal.Where(c => c.Length == 6);
            foreach (var digit in zeroSixNine)
            {
                if (four.All(d => digit.Contains(d)))
                {
                    // contains a 4, so must be a 9
                    var nine = digit.ToCharArray();
                    decoder.Add(Key(nine), 9);
                    bottomLeft = eight.Except(nine).Single();
                }
                else if (seven.All(d => digit.Contains(d)))
                {
                    // contains a 7 but not a 4 so must be 0
                    decoder.Add(Key(digit.ToCharArray()), 0);
                }
                else
                {
                    // has to be a 6
                    decoder.Add(Key(digit.ToCharArray()), 6);
                }
            }

            var twoThreeFive = Signal.Where(c => c.Length == 5);
            foreach (var digit in twoThreeFive)
            {
                if (seven.All(d => digit.Contains(d)))
                {
                    // contains a 7 so must be a 3
                    decoder.Add(Key(digit.ToCharArray()), 3);
                }
                else if (digit.Contains(bottomLeft))
                {
                    // must be a 2
                    decoder.Add(Key(digit.ToCharArray()), 2);
                }
                else
                {
                    // has to be a 5
                    decoder.Add(Key(digit.ToCharArray()), 5);
                }
            }
                        
            Console.Write($"{string.Join(" ", Digit)} | ");

            foreach (var item in Digit)
            {
                var key = Key(item.ToCharArray());
                Console.Write(decoder[key]);
                Value = (Value * 10) + decoder[key];
            }

            Console.Write($" = {Value}");
            Console.WriteLine();
        }

        private string Key(char[] input)
        {
            Array.Sort(input);
            return new string(input);
        }
    }
}
