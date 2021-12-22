using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal class Day21 : Day
    {
        public override string Name => "Day 21: Dirac Dice";

        private readonly int _playerOneStart;

        private readonly int _playerTwoStart;

        private int _100dice = 0;

        public Day21()
        {
            var input = this.InputString();

            var match = new Regex(@"Player \d starting position: (?<position>\d)");

            _playerOneStart = int.Parse(match.Match(input.First()).Groups["position"].Value);
            _playerTwoStart = int.Parse(match.Match(input.Last()).Groups["position"].Value);
        }

        private void PlayGame()
        {
            var playerScore = new int[] { 0, 0 };
            var playerPosition = new int[] { _playerOneStart, _playerTwoStart };
            var diceRolls = 0;
            
            while (playerScore[0] < 1000 && playerScore[1] < 1000)
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Write($"Player {i} rolls ");
                    diceRolls +=3;
                    var move = DiceRoll() + DiceRoll() + DiceRoll();

                    playerPosition[i] += move;
                    playerPosition[i] = playerPosition[i] % 10;

                    if (playerPosition[i] == 0)
                    {
                        playerPosition[i] = 10;
                    }

                    Console.Write($" moves to space {playerPosition[i]} ");

                    playerScore[i] += playerPosition[i];

                    Console.WriteLine($" for a total score of {playerScore[i]}" );

                    if (playerScore[i] >= 1000)
                    {
                        // Winner!
                        break;
                    }
                }
            }

            Console.WriteLine(Math.Min(playerScore[0], playerScore[1]));
            Console.WriteLine(diceRolls);

            var answer = Math.Min(playerScore[0], playerScore[1]) * diceRolls;
            Console.WriteLine(answer);
        }

        private int DiceRoll()
        {
            _100dice++;
            if (_100dice > 100)
                _100dice = 1;
            Console.Write($" {_100dice} +");
            return _100dice;
        }

        public override void FirstAnswer()
        {
            Console.WriteLine();
            PlayGame();
        }

        public override void SecondAnswer()
        {
            Console.WriteLine(2);
        }
    }
}
