namespace AdventOfCode2021
{
    internal record Day11 : Day
    {
        public override string Name => "Day 11: Dumbo Octopus";

        private readonly int[] _octopuses;
        private readonly int _length;
        private readonly int _height;
        
        public Day11()
        {
            var input = this.InputString();

            _height = input.Count();
            _length = input.First().Length;
            _octopuses = input.SelectMany(c => c.ToCharArray()).Select(d => int.Parse(d.ToString())).ToArray();
        }

        private long Steps(int stepCount, bool stopWhenSync = false)
        {
            long flashes = 0;
            for (int step = 0; step < stepCount; step++)
            {
                //Console.WriteLine($"Step {step} : Total Flashes {flashes}");
                //Debug();

                for (int octos = 0; octos < _octopuses.Length; octos++)
                {
                    _octopuses[octos]++;
                }

                var neighbourFlash = (int loc) =>
                {
                    if (loc >= 0 
                    && loc < _octopuses.Length 
                    && _octopuses[loc] != 0)
                    {
                        _octopuses[loc]++;
                    }
                };

                while (_octopuses.Max() > 9)
                {
                    for (int octos = 0; octos < _octopuses.Length; octos++)
                    {
                        if (_octopuses[octos] > 9 )
                        {
                            flashes++;
                            _octopuses[octos] = 0;

                            // increment other octopuses
                            var (row, col) = Math.DivRem(octos, _length);

                            if (col > 0)
                            {
                                neighbourFlash(octos - 1 + _length); // down left
                                neighbourFlash(octos - 1); // left
                                neighbourFlash(octos - 1 - _length); // up left
                            }

                            neighbourFlash(octos - _length); // up
                            
                            if (col < _length - 1)
                            {
                                neighbourFlash(octos + 1 - _length); // up right
                                neighbourFlash(octos + 1); // right
                                neighbourFlash(octos + 1 +_length); // down right
                            }

                            neighbourFlash(octos + _length); // down
                        }
                    }
                }

                if (stopWhenSync && _octopuses.All(c => c == _octopuses.First()))
                {
                    Console.WriteLine($"Octopus are all in sync at step {step +1 +100}");
                    break;
                }
            }
                       
            Debug();

            return flashes;
        }

        public override void FirstAnswer()
        {
            Console.WriteLine($"Answer is {Steps(100)}");
        }


        public override void SecondAnswer()
        {
            Steps(500, true);
        }

        private void Debug()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    Console.Write(_octopuses[i * _length + j]);
                }

                Console.WriteLine();
            }
        }
    }
}
