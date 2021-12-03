using System.Text;

namespace AdventOfCode2021
{
    internal class Day3 : Day
    {
        public override string Name => "day3";

        private IEnumerable<IEnumerable<char>> GetData()
        {
            var input = InputString().Select(x => x.ToArray());

            var binary = Enumerable.Range(0, input.First().Length).Select(i => input.Select(l => l[i]));

            return binary;
        }

        public override void FirstAnswer()
        {
            var data = GetData();
            var gammaString = new StringBuilder();
            

            foreach (var row in data)
            {
                var value = row.Count(x => x == '1') > row.Count(x => x == '0') ? 1 : 0;

                gammaString.Append(value);
            }
            
            var gamma = Convert.ToUInt32(gammaString.ToString(), 2);
            var epsilon = ~Convert.ToUInt32(gammaString.ToString(), 2) & 0x0000000FFF;

            // higher than 499698
            Console.WriteLine(gamma * epsilon);
        }

        public override void SecondAnswer()
        {
            var data = GetData();
            var oxygen = InputString().ToList();
            var scrubber = InputString().ToList();


            var start = 0;
            while (oxygen.Count() > 1)
            {
                var ones = oxygen.Where(x => x.Substring(start, 1) == "1").ToList();
                var zeros = oxygen.Where(x => x.Substring(start, 1) == "0").ToList();

                oxygen.Clear();
                if (ones.Count >= zeros.Count)
                {
                    oxygen = ones;
                }
                else
                {
                    oxygen = zeros;
                }
                start++;
            }

            start = 0;
            while (scrubber.Count() > 1)
            {
                var ones = scrubber.Where(x => x.Substring(start, 1) == "1").ToList();
                var zeros = scrubber.Where(x => x.Substring(start, 1) == "0").ToList();

                scrubber.Clear();
                if (ones.Count < zeros.Count)
                {
                    scrubber = ones.ToList();
                }
                else
                {
                    scrubber = zeros.ToList();
                }
                start++;
            }

            var finalOxygen = Convert.ToUInt32(oxygen.First(), 2);
            var finalScrubber = Convert.ToUInt32(scrubber.First(), 2);

            Console.WriteLine(finalOxygen * finalScrubber);
        }
    }
}
