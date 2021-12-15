namespace AdventOfCode2021
{
    internal class Day15 : Day
    {
        public override string Name => "Day 15: Chiton";

        private int Navigate(int[] map, int mapWidth)
        {
            var paths = new Dictionary<int, int> { { 0, 0 } };

            var queue = new Queue<Tuple<int,int>>();
            queue.Enqueue(new Tuple<int,int>(0, 0));

            var newBranch = new Action<int, int>((newPosition, risk) =>
            {
                var newRisk = risk + map[newPosition];

                if (paths.ContainsKey(newPosition))
                {
                    if (paths[newPosition] > newRisk)
                    {
                        paths[newPosition] = newRisk;
                        queue.Enqueue(new Tuple<int, int>(newPosition, risk + map[newPosition]));
                    }
                }
                else
                {
                    paths.Add(newPosition, newRisk);
                    queue.Enqueue(new Tuple<int, int>(newPosition, risk + map[newPosition]));
                }
            });

            while (queue.Count > 0)
            {
                var location = queue.Dequeue();

                var postition = location.Item1;
                var risk = location.Item2;

                var (_, col) = Math.DivRem(postition, mapWidth);

                if (col + 1 < mapWidth)
                {
                    // move right
                    newBranch(postition + 1, risk);
                }

                if (col > 0)
                {
                    // Move left
                    newBranch(postition - 1, risk);
                }

                if (postition - mapWidth > 0)
                {
                    // Move up
                    newBranch(postition - mapWidth, risk);
                }

                if (postition + mapWidth < map.Length)
                {
                    // Move down
                    newBranch(postition + mapWidth, risk);
                };
            }

            return paths[map.Length - 1];
        }

        public override void FirstAnswer()
        {
            var input = this.InputString();
            var map = input.SelectMany(c => c.ToCharArray()).Select(d => int.Parse(d.ToString())).ToArray();

            Console.WriteLine(Navigate(map, input.First().Length));
        }

        public override void SecondAnswer()
        {
            var input = this.InputString();

            var expandedMap = new List<int[]>();
            foreach (var line in input)
            {
                var initial = line.ToCharArray().Select(d => int.Parse(d.ToString())).ToList();
                var expandedLine = new List<int>(initial);
                for (int i = 0; i < 4; i++)
                {
                    foreach (var item in initial)
                    {
                        var newRisk = (item + i) % 9 + 1;
                        expandedLine.Add(newRisk);
                    }
                }

                expandedMap.Add(expandedLine.ToArray());
            }

            var moreMap = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                foreach (var line in expandedMap)
                {
                    var expandedLine = new List<int>();
                    foreach (var item in line)
                    {
                        var newRisk = (item + i) % 9 + 1;
                        expandedLine.Add(newRisk);
                    }

                    moreMap.Add(expandedLine.ToArray());
                }
            }
            
            expandedMap.AddRange(moreMap);

            var map = expandedMap.SelectMany(c => c).ToArray();
            var mapWidth = input.First().Length * 5;

            Console.WriteLine(Navigate(map, mapWidth));
        }
    }
}
