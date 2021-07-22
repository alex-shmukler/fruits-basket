using FruitBasket.Enums;
using FruitBasket.Models;

namespace FruitBasket.Services
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IResultsHandler _resultsHandler;

        public PlayerFactory(IResultsHandler resultsHandler)
        {
            _resultsHandler = resultsHandler;
        }

        public Player CreatePlayer(string name, PlayerType type)
        {
            return type switch
            {
                PlayerType.Random => new RandomPlayer(name, type, _resultsHandler),
                PlayerType.Memory => new MemoryPlayer(name, type, _resultsHandler),
                PlayerType.Thorough => new ThoroughPlayer(name, type, _resultsHandler),
                PlayerType.Cheater => new CheaterPlayer(name, type, _resultsHandler),
                PlayerType.ThoroughCheater => new ThoroughCheaterPlayer(name, type, _resultsHandler),
                _ => null,
            };
        }
    }
}
