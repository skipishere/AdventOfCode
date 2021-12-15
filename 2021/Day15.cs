namespace AdventOfCode2021
{
    internal class Day15 : Day
    {
        public override string Name => "Day 15: Chiton";

        private int Navigate(int[] map, int mapWidth)
        {
            var paths = new Dictionary<int, int>();
            paths.Add(0, 0);
                        
            var queue = new Queue<Tuple<int,int>>();
            queue.Enqueue(new Tuple<int,int>(0, 0));
            
            while (queue.Count > 0)
            {
                var location = queue.Dequeue();

                var postition = location.Item1;
                var risk = location.Item2;

                var (row, col) = Math.DivRem(postition, mapWidth);

                var newBranch = new Action<int> (newPosition => 
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

                if (col + 1 < mapWidth)
                {
                    // move right
                    newBranch(postition + 1);
                }

                if (col > 0)
                {
                    // Move Left
                    newBranch(postition - 1);
                }

                if (postition - mapWidth > 0)
                {
                    // Move up
                    newBranch(postition - mapWidth);
                }

                if (postition + mapWidth < map.Length)
                {
                    // Move down
                    newBranch(postition + mapWidth);
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
            var map = input.SelectMany(c => c.ToCharArray()).Select(d => int.Parse(d.ToString())).ToArray();

            var mapWidth = input.First().Length;
            
            Console.WriteLine(Navigate(map, mapWidth));
        }
    }
}
