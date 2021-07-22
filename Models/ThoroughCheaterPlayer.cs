﻿using FruitBasket.Enums;
using FruitBasket.Services;
using System;

namespace FruitBasket.Models
{
    public class ThoroughCheaterPlayer : Player
    {
        private int _previousGuess;

        public ThoroughCheaterPlayer(string name, PlayerType playerType, IResultsHandler resultsHandler)
            : base(name, playerType, resultsHandler)
        {
            _previousGuess = 39;
        }

        public override int NextGuess()
        {
            do
            {
                _previousGuess++;
            }
            while (!_resultsHandler.TryAddGuess(_previousGuess));

            _resultsHandler.AddResult(new Result
            {
                PlayerName = _name,
                Guess = _previousGuess,
                TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            });

            return _previousGuess;
        }
    }
}
