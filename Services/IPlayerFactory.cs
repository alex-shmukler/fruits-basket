using FruitBasket.Enums;
using FruitBasket.Models;

namespace FruitBasket.Services
{
    public interface IPlayerFactory
    {
        Player CreatePlayer(string name, PlayerType type);
    }
}
