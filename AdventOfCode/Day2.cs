using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal sealed class Day2 : IDay
    {
        public void Run()
        {
            var input = File.ReadLines(@"input\day2.txt");
            var report = new List<Password>();


            var pattern = new Regex(@"(?<min>\d+)-(?<max>\d+) (?<letter>\w): (?<word>\w*)");

            report.AddRange(input.Select(c => 
            {
                var match = pattern.Match(c);
                return new Password
                {
                    Minimum = int.Parse(match.Groups["min"].Value),
                    Maximum = int.Parse(match.Groups["max"].Value),
                    Letter = match.Groups["letter"].Value.First(),
                    Word = match.Groups["word"].Value,
                };
            }));

            var reportArray = report.ToArray();

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            Console.WriteLine(report.Count(c => c.IsValid));
            timer.Stop();

            Console.WriteLine($"Time taken: {timer.Elapsed}");
        }

        private class Password
        {
            public int Minimum { get; set; }
            public int Maximum { get; set; }
            public char Letter { get; set; }
            public string Word { get; set; }
            public bool IsValid { get { return SecondRuleIsValid(); } }

            private bool CheckIsValid()
            {
                var letterCount = Word.Count(c => c == Letter);
                return letterCount >= Minimum && letterCount <= Maximum;
            }

            private bool SecondRuleIsValid()
            {
                var letter1 = Word.ElementAt(Minimum - 1) == Letter;
                var letter2 = Word.ElementAt(Maximum - 1) == Letter;

                return letter1 ^ letter2;
            }
        }
    }

}
