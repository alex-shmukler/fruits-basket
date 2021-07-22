using FruitBasket.Enums;
using FruitBasket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasket.Models
{
    public class Game
    {
        private int _numberOfPlayers;
        private readonly int _basketWeight;
        private readonly IList<Player> _players;
        private readonly IResultsHandler _resultsHandler;

        public Game()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            _basketWeight = random.Next(40, 141);
            _players = new List<Player>();
            _resultsHandler = new ResultsHandler();
        }

        public void Run()
        {
            if (!TryGetNumberOfPlayers())
            {
                return;
            }

            PrintPlayerTypes();

            if (!TryGetPlayersInformation())
            {
                return;
            }

            StartGame();

            PrintResults();
        }

        private bool TryGetNumberOfPlayers()
        {
            Console.WriteLine("Please enter the number of players");

            string input = Console.ReadLine();

            if (!int.TryParse(input, out _numberOfPlayers))
            {
                Console.WriteLine("Invalid input");

                return false;
            }

            if (_numberOfPlayers < 2 || _numberOfPlayers > 8)
            {
                Console.WriteLine("Invalid number");

                return false;
            }

            return true;
        }

        private void PrintPlayerTypes()
        {
            Console.WriteLine("Player types:");

            foreach (PlayerType playerType in Enum.GetValues(typeof(PlayerType)))
            {
                Console.WriteLine($"{(int)playerType} - {playerType}");
            }
        }

        private bool TryGetPlayersInformation()
        {
            PlayerType playerType;

            IPlayerFactory playerFactory = new PlayerFactory(_resultsHandler);

            foreach (int playerNumber in Enumerable.Range(1, _numberOfPlayers))
            {
                Console.WriteLine($"Please enter information for player number {playerNumber}");

                Console.WriteLine($"Name: ");

                string name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Invalid player name");

                    return false;
                }

                Console.WriteLine($"Type: ");

                string type = Console.ReadLine();

                if (!int.TryParse(type, out int typeParsed))
                {
                    Console.WriteLine("Invalid player type");

                    return false;
                }

                try
                {
                    playerType = (PlayerType)typeParsed;
                }
                catch
                {
                    Console.WriteLine("Invalid player type");

                    return false;
                }

                _players.Add(playerFactory.CreatePlayer(name, playerType));
            }

            return true;
        }

        private void StartGame()
        {
            IList<Task> tasks = new List<Task>();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            foreach (Player player in _players)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await player.Play(_basketWeight, cancellationTokenSource);
                }));
            }

            cancellationTokenSource.CancelAfter(1500);

            Console.WriteLine("Game started :)");

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch // cancellation 
            {
            }
        }

        private void PrintResults()
        {
            Console.WriteLine("Game results: ");

            Console.WriteLine($"Weight of a fruit basket: {_basketWeight}");

            Result winner = _resultsHandler.GetWinner(_basketWeight);

            if (winner.Guess == _basketWeight)
            {
                int amount = _resultsHandler.GetWinnerAttempts(winner.PlayerName);

                Console.WriteLine($"The Winner: {winner.PlayerName} - Attempts: {amount}");
            }
            else
            {
                Console.WriteLine($"Closest to win: {winner.PlayerName} - Guess: {winner.Guess}");
            }
        }
    }
}
