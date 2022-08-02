using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    internal record Day21 : Day
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

        private void QuantumGame()
        {
            var gameQueue = new PriorityQueue<Game, long>();
            var inProgress = new Dictionary<Game, long>();
            var finishedGames = new List<Game>();

            var seedGame = new Game(position: new int[] { _playerOneStart, _playerTwoStart }, score: new int[] { 0, 0 }, copies: 0) { PlayerTurn = 0 };
            gameQueue.Enqueue(seedGame, 1);
            inProgress.Add(seedGame, 1);

            var checkAndAdd = new Action<Game, int, long>((game, newSplits, copies) => 
            {
                if (game.Score[0] >= 21 || game.Score[1] >= 21)
                {
                    // game over
                    game.Copies = newSplits * copies;
                    finishedGames.Add(game);

                    return;
                }

                if (inProgress.ContainsKey(game))
                {
                    inProgress[game] += newSplits * copies;
                }
                else
                {
                    inProgress.Add(game, newSplits * copies);
                    gameQueue.Enqueue(game, game.Score[0]);
                }
            });

            while (gameQueue.Count > 0)
            {
                var game = gameQueue.Dequeue();

                if (!inProgress.ContainsKey(game))
                {
                    // we've already done this one.
                    continue;
                }

                var count = inProgress[game];
                inProgress.Remove(game);

                // Dice end up with game split with total dice score
                // 1 x 3r
                checkAndAdd(new Game(game, 3), 1, count);

                // 3 x 4r
                checkAndAdd(new Game(game, 4), 3, count);

                // 6 x 5r
                checkAndAdd(new Game(game, 5), 6, count);

                // 7 x 6r
                checkAndAdd(new Game(game, 6), 7, count);

                // 6 x 7r
                checkAndAdd(new Game(game, 7), 6, count);

                // 3 x 8r
                checkAndAdd(new Game(game, 8), 3, count);

                // 1 x 9r
                checkAndAdd(new Game(game, 9), 1, count);
            }

            var player1Wins = finishedGames.Where(c => c.Score[0] > c.Score[1]).Sum(d => d.Copies);
            var player2Wins = finishedGames.Where(c => c.Score[0] < c.Score[1]).Sum(d => d.Copies);

            Console.WriteLine($"Player 1 wins in {player1Wins} universes");
            Console.WriteLine($"Player 2 wins in {player2Wins} universes");
        }

        public override void FirstAnswer()
        {
            Console.WriteLine();
            PlayGame();
        }

        public override void SecondAnswer()
        {
            Console.WriteLine();
            QuantumGame();
        }

        public record Game
        {
            public int[] Position { get; set; }

            public int[] Score { get; set; }

            public int PlayerTurn { get; set; }

            public long Copies { get; set; }

            public Game(int[] position, int[] score, long copies)
            {
                Position = position;
                Score = score;
                Copies = copies;
            }

            public Game(Game game, int move)
            {
                Position = new int[] { game.Position[0], game.Position[1] };
                Score = new int[] { game.Score[0], game.Score[1] };
                Copies = game.Copies;
                PlayerTurn = game.PlayerTurn;

                Position[PlayerTurn] += move;
                Position[PlayerTurn] = Position[PlayerTurn] % 10;

                if (Position[PlayerTurn] == 0)
                {
                    Position[PlayerTurn] = 10;
                }

                Score[PlayerTurn] += Position[PlayerTurn];

                PlayerTurn = PlayerTurn == 1 ? 0: 1;
            }
        }
    }
}
