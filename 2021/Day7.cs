namespace AdventOfCode2021
{
    internal class Day7 : Day
    {
        public override string Name => "Day 7: The Treachery of Whales";

        private readonly IEnumerable<int> _crabPosition = new List<int>();

        public Day7()
        {
            _crabPosition = this.InputString().First().Split(',').Select(c => int.Parse(c));
        }

        public int CalculateFuel(bool triangular)
        {
            var answer = new Dictionary<int, int>();

            for (int i = 1; i < _crabPosition.Max(); i++)
            {
                var fuelSpent = _crabPosition.Sum(c =>
                {
                    var movement = (c > i) ? c - i : i - c;
                    
                    if (triangular)
                    {
                        return movement * (movement + 1) / 2;
                    }

                    return movement;
                });

                answer.Add(i, fuelSpent);
            }

            return answer.Values.Min();
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(CalculateFuel(false));
        }


        public override void SecondAnswer()
        {
            Console.WriteLine(CalculateFuel(true));
        }
    }
}
