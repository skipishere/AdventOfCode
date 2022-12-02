namespace AdventOfCode2022
{
    internal record Day01 : Day
    {
        public override string Name => "Day 1: Calorie Counting";

        private readonly List<int> elves = new();

        public Day01()
        {
            var elfCalories = 0;
            foreach (var calorie in InputString())
            {
                if (calorie == string.Empty)
                {
                    elves.Add(elfCalories);
                    elfCalories = 0; 
                }
                else
                {
                    elfCalories += int.Parse(calorie);
                }
            }
        }

        public override string FirstAnswer()
        {
            return elves.Max().ToString();
        }

        public override string SecondAnswer()
        {
            return elves
                .OrderByDescending(c => c)
                .Take(3)
                .Sum()
                .ToString();
        }
    }
}
