namespace AdventOfCode2021
{
    internal class Day10 : Day
    {
        public override string Name => "Day 10: Syntax Scoring";

        private readonly IEnumerable<string> _lines;

        public Day10()
        {
            _lines = this.InputString();        
        }

        public long CalculateScore(bool corrupt)
        {
            var corruptScore = 0;
            var incompleteScores = new List<long>();

            foreach (var line in _lines)
            {
                var isCorrupt = false;

                var bracket = new Stack<char>();
                foreach (var character in line)
                {
                    if (character is '(' or '[' or '{' or '<')
                    {
                        bracket.Push(character);
                    }
                    else if (!IsPair(character, bracket.Pop(), out var newScore))
                    {
                        // hit the error, score and move to next line.
                        corruptScore += newScore;
                        isCorrupt = true;
                        break;
                    }
                }

                if (!isCorrupt)
                {
                    // Incomplete, time to work out the ending...
                    long incomplete = 0;
                    
                    foreach (var unclosed in bracket)
                    {
                        CloseBracket(unclosed, ref incomplete);
                    }
                
                    incompleteScores.Add(incomplete);
                }
            }

            return corrupt ? corruptScore : incompleteScores.OrderBy(c => c).ElementAt((incompleteScores.Count - 1) / 2);
        }

        public override void FirstAnswer()
        {
            Console.WriteLine(CalculateScore(true));
        }


        public override void SecondAnswer()
        {
            Console.WriteLine(CalculateScore(false));
        }

        private bool IsPair(char input, char lastOpen, out int score)
        {
            switch (input)
            {
                case ')':
                    score = 3;
                    return lastOpen == '(';
                case ']':
                    score = 57;
                    return lastOpen == '[';
                case '}':
                    score = 1197;
                    return lastOpen == '{';
                case '>':
                    score = 25137;
                    return lastOpen == '<';
                default:
                    throw new Exception($"Invalid character {input}");
            }
        }

        private void CloseBracket(char input, ref long score)
        {
            var localScore = input switch
            {
                '(' => 1,
                '[' => 2,
                '{' => 3,
                '<' => 4,
                _ => throw new Exception($"Invalid character {input}"),
            };
            score = score * 5 + localScore;
        }

    }
}
