namespace AdventOfCode2021
{
    internal class Day04 : Day
    {
        public override string Name => "Day 4: Giant Squid";

        private readonly List<int> _calledNumbers;

        private readonly List<Board> _boards = new();

        public Day04()
        {
            var input = this.InputString();

            _calledNumbers = input.First().Split(',').Select(c => int.Parse(c)).ToList();
            
            var bingoBoards = input.Skip(1).Where(c => !string.IsNullOrWhiteSpace(c));

            var boardNumber = new List<int>();
            foreach (var data in bingoBoards)
            {
                boardNumber.AddRange(data.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)));
                
                if (boardNumber.Count == 25)
                {
                    _boards.Add(new Board(boardNumber));
                    boardNumber.Clear();
                }
            }
        }

        /// <summary>
        /// Looks for winning boards
        /// </summary>
        /// <param name="onlyOneWinner">If true, stop when we have the 1st winner, otherwise keep going</param>
        public void GetWinner(bool onlyOneWinner)
        {
            // Start with the 5th number, as you can't win before then.
            for (int i = 5; i < _calledNumbers.Count; i++)
            {
                var call = _calledNumbers.Take(i);
                foreach (var board in _boards.Where(c => !c.HasWon))
                {
                    if (board.Bingo(call))
                    {
                        var score = board.Numbers.Except(call).Sum();

                        Console.WriteLine(score * call.Last());

                        if (onlyOneWinner)
                        {
                            return;
                        }
                    }
                }
            }

            return;
        }

        public override void FirstAnswer()
        {
            GetWinner(true);
        }


        public override void SecondAnswer()
        {
            GetWinner(false);
        }

        internal class Board
        {
            public IEnumerable<int> Numbers { get; private set; }

            public Board(IEnumerable<int> numbers)
            {
                Numbers = new List<int>(numbers);
            }

            /// <summary>
            /// Indicates if the board has already won.
            /// </summary>
            public bool HasWon { get; private set; }

            public bool Bingo(IEnumerable<int> numbersCalled)
            {
                if (HasWon)
                {
                    // Can't win twice, so skip as marked as won.
                    return false;
                }

                for (int rows = 0; rows < 5; rows++)
                {
                    var row = this.Numbers.Skip(rows * 5).Take(5);

                    var matches = numbersCalled.Intersect(row);
                    if (matches.Count() == 5)
                    {
                        HasWon = true;
                        return true;
                    }
                }

                for (int cols = 0; cols < 5; cols++)
                {
                    var col = new List<int>(5);
                    for (int i = 0; i < 5; i++)
                    {
                        col.Add(this.Numbers.ElementAt(cols + (i * 5)));
                    }

                    var matches = numbersCalled.Intersect(col);
                    if (matches.Count() == 5)
                    {
                        HasWon = true;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
