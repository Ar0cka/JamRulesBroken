using Player.PlayerProviders;
using UnityEditor.VersionControl;

namespace UISystem
{
    public interface IShop
    {
        void EnterToShop(IStateProvider stateProvider);
    }
}