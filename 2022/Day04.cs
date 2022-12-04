namespace AdventOfCode2022;

internal record Day04 : Day
{
    public override string Name => "Day 2: Camp Cleanup";

    private new List<Tuple<Job, Job>> _pairs = new();

    public Day04()
    {
        foreach (var line in InputString())
        {
            var set = line.Split(',');
            _pairs.Add(new Tuple<Job, Job>(new Job(set[0]), new Job(set[1])));
        }        
    }

    public override object FirstAnswer()
    {
        var sets =
            _pairs.Count(c =>
                c.Item1.Start >= c.Item2.Start && c.Item1.Finish <= c.Item2.Finish
                || c.Item2.Start >= c.Item1.Start && c.Item2.Finish <= c.Item1.Finish);
        
        return sets;
    }

    public override object SecondAnswer()
    {
        var sets =
            _pairs.Count(c =>
                (c.Item1.Start >= c.Item2.Start && c.Item1.Start <= c.Item2.Finish)
                || (c.Item1.Finish >= c.Item2.Start && c.Item1.Finish <= c.Item2.Finish)
                || (c.Item2.Start >= c.Item1.Start && c.Item2.Start <= c.Item1.Finish)
                || (c.Item2.Finish >= c.Item1.Start && c.Item2.Finish <= c.Item1.Finish));

        return sets;
    }

    private record Job
    {
        public int Start { get; set; }
        public int Finish { get; set; }

        public Job(string input)
        {
            var split = input.Split("-");
            Start = int.Parse(split[0]);
            Finish = int.Parse(split[1]);
        }
    }
}
