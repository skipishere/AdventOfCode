using System.Collections.Concurrent;

namespace AdventOfCode2021
{
    internal class Day12 : Day
    {
        public override string Name => "Day 12: Passage Pathing";

        private readonly Dictionary<string, List<string>> _paths = new();

        private readonly ConcurrentBag<Route> _routes = new();
        
        public Day12()
        {
            foreach (var line in this.InputString())
            {
                var rooms = line.Split('-');

                AddPaths(rooms[0], rooms[1]);
                AddPaths(rooms[1], rooms[0]);
            }

            void AddPaths(string room1, string room2)
            {
                if (room2 == "start")
                {
                    return;
                }

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

        private int CalculatePath(bool canVisitASmallCaveTwice)
        {
            _routes.Clear();

            Parallel.ForEach(_paths["start"], cave =>
            {
                Visit(cave, new Route(new List<string> { "start" }), canVisitASmallCaveTwice);
            });
            
            return _routes.Count;
        }

        private void Visit(string startCave, Route route, bool canVisitASmallCaveTwice)
        {
            route.Caves.Add(startCave);

            if (startCave == "end")
            {
                _routes.Add(route);
                return;
            }

            foreach (var cave in _paths[startCave].Where(cave => !IsSmallCave(cave)
                                   || !route.Caves.Contains(cave)
                                   || canVisitASmallCaveTwice && !route.HaveVisitedSmallCaveTwice))
            {
                var myRoute = new Route(route.Caves);
                Visit(cave, myRoute, canVisitASmallCaveTwice);
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
            Console.WriteLine(CalculatePath(true));
        }

        private class Route
        {
            public List<string> Caves { get; set; }

            public bool HaveVisitedSmallCaveTwice => Caves.Where(c => IsSmallCave(c))
                                                          .GroupBy(c => c)
                                                          .Max(d => d.Count()) > 1;

            public Route(List<string>? caves = null)
            {
                Caves = caves == null ? new() : new List<string>(caves);
            }
        }
    }
}
