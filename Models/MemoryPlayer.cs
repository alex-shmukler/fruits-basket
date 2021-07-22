using FruitBasket.Enums;
using FruitBasket.Services;
using System;
using System.Collections.Generic;

namespace FruitBasket.Models
{
    public class MemoryPlayer : Player
    {
        private readonly Random _random;
        private readonly IList<int> _previousGuesses;
        public MemoryPlayer(string name, PlayerType playerType, IResultsHandler resultsHandler)
            : base(name, playerType, resultsHandler)
        {
            _random = new Random(DateTime.Now.Millisecond);
            _previousGuesses = new List<int>();
        }

        public override int NextGuess()
        {
            int guess;

            do
            {
                guess = _random.Next(40, 141);
            }
            while (_previousGuesses.Contains(guess));

            _previousGuesses.Add(guess);

            _resultsHandler.TryAddGuess(guess);

            _resultsHandler.AddResult(new Result
            {
                PlayerName = _name,
                Guess = guess,
                TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            });

            return guess;
        }
    }
}
