using Game.World.Player.StateController;

namespace Game.World.Player.Interfaces
{
    public interface IStateProvider
    {
        void SwitchPlayerState(PlayerStates playerState, bool currentState);
    }
}