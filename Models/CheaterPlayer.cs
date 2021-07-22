using FruitBasket.Enums;
using FruitBasket.Services;
using System;

namespace FruitBasket.Models
{
    public class CheaterPlayer : Player
    {
        private readonly Random _random;

        public CheaterPlayer(string name, PlayerType playerType, IResultsHandler resultsHandler)
            : base(name, playerType, resultsHandler)
        {
            _random = new Random(DateTime.Now.Millisecond);
        }

        public override int NextGuess()
        {
            int guess;

            do
            {
                guess = _random.Next(40, 141);
            }
            while (!_resultsHandler.TryAddGuess(guess));

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
