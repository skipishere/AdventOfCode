namespace AdventOfCode2021
{
    internal class Day6 : Day
    {
        public override string Name => "Day 6: Lanternfish";

        private IList<Fish> _initialSchool = new List<Fish>();

        public Day6()
        {
            var input = this.InputString().First().Split(',').Select(c => int.Parse(c));

            _initialSchool = input.GroupBy(c => c).Select(f => new Fish { Age = f.Key, Number = f.Count() }).ToList();
        }

        public long CalculateFish(int days)
        {
            for (int i = 0; i < days; i++)
            {
                long newFish = 0;
                foreach (var item in _initialSchool)
                {
                    newFish += item.Decrement();
                }

                _initialSchool.Add(new Fish { Age = 8, Number = newFish });
            }

            return _initialSchool.Sum(c => c.Number);
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(CalculateFish(80));
        }


        public override void SecondAnswer()
        {
            // We haven't reset, so don't just dump in 256 :facepalm:
            Console.WriteLine(CalculateFish(256-80));
        }

        public class Fish
        {
            public int Age { get; set; }

            public long Number { get ; set; }

            public long Decrement()
            {
                Age--;

                if (Age == -1)
                {
                    Age = 6;
                    return Number;
                }

                return 0;
            }
        }
    }
}
