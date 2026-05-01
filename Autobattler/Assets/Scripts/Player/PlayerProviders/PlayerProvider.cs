using Player.StateController;
using UnityEngine;

namespace Player.PlayerProviders
{
    public class PlayerProvider : MonoBehaviour, IStateProvider
    {
        private PlayerStateController _playerStateController;

        public void InitializePlayerProvider(PlayerProviderData playerProvidersSystems)
        {
            _playerStateController = playerProvidersSystems.PlayerStateController;
        }

        public void SwitchPlayerState(PlayerStates playerState, bool currentState)
        {
            _playerStateController.UpdatePlayerState(playerState, currentState);
        }
    }

    public struct PlayerProviderData
    {
        public PlayerStateController PlayerStateController;
    }
}