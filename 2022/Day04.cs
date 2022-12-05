namespace AdventOfCode2022;

internal record Day04 : Day
{
    public override string Name => "Day 4: Camp Cleanup";

    private readonly List<Tuple<Job, Job>> _pairs = new();

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
        return _pairs.Count(c => c.Item1.Within(c.Item2));
    }

    public override object SecondAnswer()
    {        
        return _pairs.Count(c => c.Item1.Intersec(c.Item2));
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

        public bool Within(Job otherJob)
        {
            return otherJob.Start >= Start && otherJob.Finish <= Finish
                || Start >= otherJob.Start && Finish <= otherJob.Finish;
        }

        public bool Intersec(Job otherJob)
        {
            return
                Math.Max(otherJob.Start, this.Start) <= Math.Min(otherJob.Finish, this.Finish);
        }
    }
}
