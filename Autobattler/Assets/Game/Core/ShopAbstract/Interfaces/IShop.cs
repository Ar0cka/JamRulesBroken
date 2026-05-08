using Game.World.Player.Interfaces;

namespace Game.Core.ShopAbstract.Interfaces
{
    public interface IShop
    {
        void EnterToShop(IStateProvider stateProvider, IPlayerContainer playerContainer);
    }
}