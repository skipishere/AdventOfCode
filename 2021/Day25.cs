namespace AdventOfCode2021
{
    internal record Day25 : Day
    {
        public override string Name => "Day 25: Sea Cucumber";

        private readonly List<char> _cucumbers;
        private readonly int _width;
        private readonly int _height;

        public Day25()
        {
            var input = this.InputString();

            _height = input.Count();
            _width = input.First().Length;
            _cucumbers = input.SelectMany(c => c.ToCharArray()).ToList();
        }

        private int Migration()
        {
            var steps = 1;
            
            while(MoveEast() | MoveSouth())
            {
                steps++;
            }

            return steps;
        }

        private void Display(int step)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Step #{step}");

            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    Console.Write(_cucumbers[row * _width + col]);
                }

                Console.WriteLine();
            }
        }

        private bool MoveEast()
        {
            var movers = new List<Tuple<int, int>>();

            for (int row = 0; row < _height; row++)
            {
                for (int col = 0; col < _width; col++)
                {
                    var position = row * _width + col;
                    var nextPosition = position + 1;
                    if (col + 1 == _width)
                    {
                        nextPosition -= _width;
                    }

                    if (_cucumbers[position] == '>' && _cucumbers[nextPosition] == '.')
                    {
                        movers.Add(new Tuple<int, int>(position, nextPosition));
                    }
                }

            }

            foreach (var mover in movers)
            {
                _cucumbers[mover.Item1] = '.';
                _cucumbers[mover.Item2] = '>';
            }

            return movers.Any();
        }

        private bool MoveSouth()
        {
            var movers = new List<Tuple<int, int>>();

            for (int col = 0; col < _width; col++)
            {
                for (int row = 0; row < _height; row++)
                {
                    var position = (row * _width) + col;
                    var nextPosition = position + _width;
                    if (row + 1 == _height)
                    {
                        nextPosition = col;
                    }

                    if (_cucumbers[position] == 'v' && _cucumbers[nextPosition] == '.')
                    {
                        movers.Add(new Tuple<int, int>(position, nextPosition));
                    }
                }

            }

            foreach (var mover in movers)
            {
                _cucumbers[mover.Item1] = '.';
                _cucumbers[mover.Item2] = 'v';
            }

            return movers.Any();
        }

        public override void FirstAnswer()
        {
            Console.WriteLine($"Free parking after {Migration()} steps.");
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(2);
        }
    }
}
