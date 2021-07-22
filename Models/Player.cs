using FruitBasket.Enums;
using FruitBasket.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FruitBasket.Models
{
    public abstract class Player
    {
        protected int _guessCounter;
        protected string _name;
        protected PlayerType _playerType;
        protected IResultsHandler _resultsHandler;

        public Player(string name, PlayerType playerType, IResultsHandler resultsHandler)
        {
            _name = name;
            _playerType = playerType;
            _resultsHandler = resultsHandler;
            _guessCounter = 0;
        }

        public async Task Play(int basketWeight, CancellationTokenSource cancellationTokenSource)
        {
            int guess;

            CancellationToken cancellationToken = cancellationTokenSource.Token;

            while (_guessCounter < 101 && !cancellationToken.IsCancellationRequested)
            {
                _guessCounter++;

                guess = NextGuess();

                if (basketWeight == guess)
                {
                    cancellationTokenSource.Cancel();
                }
                else
                {
                    await Task.Delay(Math.Abs(basketWeight - guess), cancellationToken);
                }
            }

            if (_guessCounter > 100)
            {
                cancellationTokenSource.Cancel();
            }
        }

        public abstract int NextGuess();
    }
}
