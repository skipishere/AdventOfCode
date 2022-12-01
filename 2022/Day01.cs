namespace AdventOfCode2022
{
    internal record Day01 : Day
    {
        public override string Name => "Day 1: Calorie Counting";

        private List<Elf> elves = new();

        public Day01()
        {
            var currentElf = new Elf();
            
            foreach (var calorie in InputString())
            {
                if (calorie == string.Empty)
                {
                    elves.Add(currentElf);
                    currentElf = new Elf();
                }
                else
                {
                    currentElf.Add(calorie);
                }
            }

            elves.Add(currentElf);
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(elves.Max(c => c.TotalCalories));
        }

        public override void SecondAnswer()
        {
            var total = elves
                .OrderByDescending(c => c.TotalCalories)
                .Take(3)
                .Sum(c => c.TotalCalories);

            Console.WriteLine(total);
        }

        private record Elf
        {
            public List<int> Calories = new();

            public int TotalCalories => this.Calories.Sum();

            public void Add(string calorie)
            {
                Calories.Add(int.Parse(calorie));
            }
        }
    }

}
