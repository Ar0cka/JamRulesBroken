using Player.StateController;

namespace Player.PlayerProviders
{
    public interface IStateProvider
    {
        void SwitchPlayerState(PlayerStates playerState, bool currentState);
    }
}