using FruitBasket.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace FruitBasket.Services
{
    public class ResultsHandler : IResultsHandler
    {
        private readonly SynchronizedList<int> _guesses;
        private readonly ConcurrentBag<Result> _results;

        public ResultsHandler()
        {
            _guesses = new SynchronizedList<int>();
            _results = new ConcurrentBag<Result>();
        }

        public void AddResult(Result result)
        {
            _results.Add(result);
        }

        public bool TryAddGuess(int guess)
        {
            return _guesses.TryAdd(guess);
        }

        public Result GetWinner(int basketWeight)
        {
            return _results.OrderBy(x => Math.Abs(basketWeight - x.Guess))
                           .ThenBy(y => y.TimeStamp)
                           .First();
        }

        public int GetWinnerAttempts(string name) // for a better performace can be done with int (like Id)
        {
            return _results.Select(x => x.PlayerName == name)
                           .Count();
        }
    }
}
