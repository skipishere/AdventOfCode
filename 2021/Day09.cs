namespace AdventOfCode2021
{
    internal class Day09 : Day
    {
        public override string Name => "Day 9: Smoke Basin";

        private readonly int[] _heightMap;
        private readonly int _length;
        private readonly int _height;
        private readonly List<int> _lowPoint = new();

        public Day09()
        {
            var input = this.InputString();

            _heightMap = input.SelectMany(c => c.ToCharArray()).Select(d => int.Parse(d.ToString())).ToArray();
            _length = input.First().Length;
            _height = input.Count();
        }

        public override void FirstAnswer()
        {
            for (int i = 0; i < _heightMap.Length; i++)
            {
                var (row, col) = Math.DivRem(i, _length);

                var current = _heightMap[i];
                var left = (col == 0) ? 9 : _heightMap[i - 1];
                var right = (col == _length - 1) ? 9 : _heightMap[i + 1];
                var above = (row < 1) ? 9 : _heightMap[i - _length];
                var below = (row == _height - 1) ? 9 : _heightMap[i + _length];

                if (current < left 
                    && current < right 
                    && current < above 
                    && current < below)
                {
                    _lowPoint.Add(i);
                }
            }

            // Need to add 1 to each low point value...
            var score = _lowPoint.Sum(point => _heightMap[point]) + _lowPoint.Count();

            Console.WriteLine(score);
        }


        public override void SecondAnswer()
        {
            var basins = new List<int>();

            foreach (var startPoint in _lowPoint)
            {
                var visited = new List<int>();
                
                var toCheck = new Stack<int>();
                toCheck.Push(startPoint);

                var checkSurvey = (int loc) =>
                {
                    if (_heightMap[loc] != 9 && !visited.Contains(loc) && !toCheck.Contains(loc))
                    {
                        toCheck.Push(loc);
                    }
                };
                
                do
                {
                    var currentLocation = toCheck.Pop();
                    
                    var (row, col) = Math.DivRem(currentLocation, _length);
                    
                    if (col > 0)
                    {
                        checkSurvey(currentLocation - 1);
                    }

                    if (col < _length - 1)
                    {
                        checkSurvey(currentLocation + 1);
                    }

                    if (row > 0)
                    {
                        checkSurvey(currentLocation - _length);
                    }

                    if (row < _height - 1)
                    {
                        checkSurvey(currentLocation + _length);
                    }

                    // We've finished visiting, so mark it as done.
                    visited.Add(currentLocation);

                } while (toCheck.Count > 0);

                basins.Add(visited.Count);
            }

            var total = 1;
            foreach (var value in basins.OrderByDescending(c => c).Take(3))
            {
                total *= value;
            }

            Console.WriteLine(total);
        }
    }
}
