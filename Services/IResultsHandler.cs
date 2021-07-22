using FruitBasket.Models;

namespace FruitBasket.Services
{
    public interface IResultsHandler
    {
        void AddResult(Result result);

        bool TryAddGuess(int guess);

        Result GetWinner(int basketWeight);

        int GetWinnerAttempts(string name);
    }
}
