﻿namespace AdventOfCode2021
{
    internal record Day07 : Day
    {
        public override string Name => "Day 7: The Treachery of Whales";

        private readonly IEnumerable<int> _crabPosition = new List<int>();

        public Day07()
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
                    return triangular ? movement * (movement + 1) / 2 : movement;
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
