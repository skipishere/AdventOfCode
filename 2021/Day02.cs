namespace AdventOfCode2021
{
    internal record Day02 : Day
    {
        public override string Name => "Day 2: Dive!";

        private List<Tuple<string, int>> GetData()
        {
            var directions = new List<Tuple<string, int>>();
            var rawDirections = InputString();

            foreach (var rawDirection in rawDirections)
            {
                var clean = rawDirection.Split(' ');
                directions.Add(new Tuple<string, int>(clean[0], int.Parse(clean[1])));
            }

            return directions;
        }

        public override void FirstAnswer()
        {
            var directions = GetData();
            var depth = 0;
            var horizontal = 0;


            foreach(var direction in directions)
            {
                switch (direction.Item1)
                {
                    case "forward":
                        horizontal+= direction.Item2;
                        break;
                    case "down":
                        depth += direction.Item2;
                        break;
                    case "up":
                        depth -= direction.Item2;
                        break;
                }
            }

            Console.WriteLine(depth*horizontal);
        }

        public override void SecondAnswer()
        {
            var directions = GetData();
            var depth = 0;
            var horizontal = 0;
            var aim = 0;

            foreach (var direction in directions)
            {
                switch (direction.Item1)
                {
                    case "forward":
                        horizontal += direction.Item2;
                        depth += aim * direction.Item2;
                        break;
                    case "down":
                        aim += direction.Item2;
                        break;
                    case "up":
                        aim -= direction.Item2;
                        break;
                }
            }

            Console.WriteLine(depth * horizontal);
        }
    }
}
