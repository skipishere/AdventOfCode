using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day5 : IDay
    {
        private static int maxRow = 127;
        private static int maxColumn = 7;

        public void Run()
        {
            var input = File.ReadLines(@"input\day5.txt");
            var seats = new List<Seat>();

            foreach (var location in input)
            {
                seats.Add(new Seat(location));
            }

            var orderedSeats = seats.OrderBy(c => c.Id);
            Console.WriteLine(orderedSeats.Last().Id);

            var missingId = new List<int>();

            var previous = orderedSeats.First().Id;
            for (int i = 1; i < orderedSeats.Count(); i++)
            {
                var next = orderedSeats.ElementAt(i);
                if (next.Id != previous+1)
                {
                    Console.WriteLine($"Missing Seat Id: {previous + 1}");
                }

                previous = next.Id;
            }

        }

        private class Seat
        {
            public int Row { get; private set; }

            public int Column { get; private set; }

            public int Id 
            {
                get 
                {
                    return this.Row * 8 + this.Column;
                } 
            }

            public Seat(string code)
            {
                this.Row = ConvertRow(code.Substring(0,7));
                this.Column = CovertColumn(code.Substring(7, 3));

            }

            private int ConvertRow(string location)
            {
                return BinaryConvert(location, 'F', 'B', maxRow);
            }

            private int CovertColumn(string location)
            {
                return BinaryConvert(location, 'L', 'R', maxColumn);
            }

            private int BinaryConvert(string value, char decrement, char increment, int startMax)
            {
                var lower = 0;
                var upper = startMax;

                foreach (var code in value)
                {
                    var number = (upper - lower + 1) / 2;

                    if (code == decrement)
                    {
                        // lower stays the same
                        upper -= number;
                    }
                    else if(code == increment)
                    {
                        // upper doesn't change
                        lower += number;
                    }
                }

                return lower;
            }
        }

    }
}
