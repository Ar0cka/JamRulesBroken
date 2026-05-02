using Player.Containers;
using Player.PlayerProviders;

namespace UISystem.Shops.Interfaces
{
    public interface IShop
    {
        void EnterToShop(IStateProvider stateProvider, IPlayerContainer playerContainer);
    }
}