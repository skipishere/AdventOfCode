using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    internal class Day4 : Day
    {

        public override string Name => "day4";


        private List<int> _calledNumbers;

        private List<Board> _boards = new List<Board>();

        public Day4()
        {
            var input = this.InputString();

            _calledNumbers = input.First().Split(',').Select(c => int.Parse(c)).ToList();

            //var boardBuilder = new StringBuilder();
            var boardNumber = new List<int>();
            foreach (var data in input.Skip(2))
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    _boards.Add(new Board(boardNumber));
                    boardNumber.Clear();
                }
                else
                {
                    boardNumber.AddRange(data.Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(c => int.Parse(c)));
                }
            }

            // Don't forget the last board, added an extra return to the input...
        }

        public Board GetWinner()
        {
            for (int i = 5; i < _calledNumbers.Count; i++)
            {
                foreach (var board in _boards)
                {
                    if (board.Bingo(_calledNumbers.Take(i)))
                    {
                        Console.WriteLine(string.Join(" ", board.Numbers));

                        // winning number
                        var finalNumber = _calledNumbers.Skip(i - 1).First();
                        var score = board.Numbers.Except(_calledNumbers.Take(i));

                        Console.WriteLine(score.Sum() * finalNumber);

                        return board;
                    }
                }
            }

            throw new Exception("Someone needs to have won by now...");
        }

        public override void FirstAnswer()
        {
            var winner = GetWinner();

            Console.WriteLine();
        }
    

        public override void SecondAnswer()
        {

            Console.WriteLine();
        }

        internal class Board
        {
            public IEnumerable<int> Numbers { get; set; }

            public Board(IEnumerable<int> numbers)
            {
                Numbers = new List<int>(numbers);
            }

            public bool Bingo(IEnumerable<int> numbersCalled)
            {
                for (int rows = 0; rows < 5; rows++)
                {
                    var row = this.Numbers.Skip(rows*5).Take(5);

                    var matches = numbersCalled.Intersect(row);
                    if (matches.Count() == 5)
                    {
                        return true;
                    }
                }

                for (int cols = 0; cols < 5; cols++)
                {
                    var col = new List<int>(5);
                    for (int i = 0; i < 5; i++)
                    {
                        col.Add(this.Numbers.ElementAt(cols + (i*5)));
                    }

                    var matches = numbersCalled.Intersect(col);
                    if (matches.Count() == 5)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
