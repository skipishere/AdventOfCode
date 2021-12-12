namespace AdventOfCode2021
{
    internal class Day12 : Day
    {
        public override string Name => "Day 12: Passage Pathing";

        private readonly Dictionary<string, List<string>> _paths = new();

        private readonly List<Route> _routes = new();

        public Day12()
        {
            var input = this.InputString();

            foreach (var line in input)
            {
                var rooms = line.Split('-');

                AddPaths(rooms[0], rooms[1]);
                AddPaths(rooms[1], rooms[0]);
            }

            void AddPaths(string room1, string room2)
            {
                if (_paths.TryGetValue(room1, out var connection))
                {
                    connection.Add(room2);
                }
                else
                {
                    _paths.Add(room1, new List<string> { room2 });
                }
            }
        }

        public int CalculatePath(bool canVisitASmallCaveTwice)
        {            
            Visit("start", new Route(), canVisitASmallCaveTwice);

            return _routes.Count(c => c.ReachedEnd);
        }

        private void Visit(string startCave, Route route, bool canVisitASmallCaveTwice)
        {
            route.Caves.Add(startCave);

            if (startCave == "end")
            {
                _routes.Add(route);
                return;
            }

            foreach (var cave in _paths[startCave].Where(c => c != "start"))
            {
                var myRoute = new Route(route.Caves);
                if (IsSmallCave(cave)
                    && myRoute.Caves.Contains(cave)
                    && (!canVisitASmallCaveTwice || myRoute.HaveVisitedSmallCaveTwice))
                {
                    // We've already been in this small cave (or twice in another small cave for part 2), abort.
                    _routes.Add(myRoute);
                }
                else
                {
                    Visit(cave, myRoute, canVisitASmallCaveTwice);
                }
            }
        }

        private static bool IsSmallCave(string cave)
        {
            return cave.ToLowerInvariant() == cave;
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(CalculatePath(false));
        }

        public override void SecondAnswer()
        {
            _routes.Clear();
            Console.WriteLine(CalculatePath(true));
        }

        private class Route
        {
            public List<string> Caves { get; set; } = new List<string>();

            public bool ReachedEnd { get { return Caves.Last() == "end"; } }

            public bool HaveVisitedSmallCaveTwice
            {
                get
                {
                    return Caves.Where(c => IsSmallCave(c)).GroupBy(c => c).Max(d => d.Count()) > 1;
                }
            }

            public Route(List<string>? caves = null)
            {
                if (caves != null)
                {
                    this.Caves = new List<string>(caves);
                }
            }
        }
    }
}
