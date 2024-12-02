namespace AdventOfCode2022;

internal partial record Day11 : Day
{
    public override string Name => "Day 11: Monkey in the Middle";

    private List<Monkey> _monkeys = new();

    public Day11()
    {
        foreach (var input in InputString().Chunk(6))
        {
            _monkeys.Add(new Monkey(input));
        }
    }

    public override object FirstAnswer()
    {
        for (int i = 0; i < 20; i++)
        {

        }

        return "";
    }

    public override object SecondAnswer()
    {
        return "";
    }

    private record Monkey
    {
        //Monkey 0:
        //Starting items: 54, 98, 50, 94, 69, 62, 53, 85
        //Operation: new = old * 13
        //Test: divisible by 3
        //If true: throw to monkey 2
        //If false: throw to monkey 1
        public int Id { get; set; }
        public Queue<int> Items { get; set; }

        private int trueId;
        private int falseId;
        private Action operation;
        private int test;

        public int ItemCount { get; private set; }

        public Monkey(string[] input)
        {
            //Id = input[0].Trim("");
        }

        public void Inspect()
        {

        }
    }
}
