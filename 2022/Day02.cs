namespace AdventOfCode2022
{
    internal record Day02 : Day
    {
        public override string Name => "Day 2: Rock Paper Scissors";
        private readonly Dictionary<string, Game> gameSets = new();

        public Day02()
        {
            foreach (var gameInput in InputString())
            {
                if (gameSets.ContainsKey(gameInput))
                {
                    gameSets[gameInput].Copies++;
                }
                else
                {
                    gameSets.Add(gameInput, new Game(gameInput));
                }                    
            }
        }

        public override object FirstAnswer()
        {
            return gameSets.Sum(c => c.Value.Score() * c.Value.Copies);
        }

        public override object SecondAnswer()
        {
            return gameSets.Sum(c => c.Value.Score(true) * c.Value.Copies);
        }

        private record Game
        {
            private readonly RPS _elfHand;
            private readonly RPS _partOneHand;
            private readonly Result _result;

            public enum RPS
            {
                Rock = 1,
                Paper = 2,
                Scissors = 3
            }

            public enum Result
            {
                Win,
                Draw,
                Lose
            }

            private RPS ConvertToHand(string input)
            {
                return input switch
                {
                    "A" or "X" => RPS.Rock,
                    "B" or "Y" => RPS.Paper,
                    "C" or "Z" => RPS.Scissors
                };
            }
        
            public Game(string input)
            {
                var hands = input.Split(" ");

                _elfHand = ConvertToHand(hands[0]);
                _partOneHand = ConvertToHand(hands[1]);
                _result = hands[1] switch
                {
                    "X" => Result.Lose,
                    "Y" => Result.Draw,
                    "Z" => Result.Win,
                };
            }

            public int Copies { get; set; } = 1;
            
            public int Score(bool alterHand = false)
            {
                RPS myHand = 0;

                if (alterHand)
                {
                    switch (_result)
                    {
                        case Result.Lose:
                            myHand = _elfHand - 1;
                            if (myHand <= 0) 
                            {
                                myHand = RPS.Scissors; 
                            }
                            break;

                        case Result.Draw:
                            myHand = _elfHand;
                            break;

                        case Result.Win:
                            myHand = _elfHand + 1;
                            if ((int)myHand > 3) 
                            {
                                myHand = RPS.Rock;
                            }
                            break;
                    }
                }
                else
                {
                    myHand = _partOneHand;
                }

                var handScore = (int)myHand;
                if (myHand == _elfHand)
                {
                    return handScore + 3;
                }

                if (myHand == RPS.Scissors && _elfHand == RPS.Rock)
                {
                    return handScore;
                }

                if (myHand > _elfHand
                    || myHand == RPS.Rock && _elfHand == RPS.Scissors)
                {
                    return handScore + 6;
                }

                return handScore;
            }
        }
    }
}
