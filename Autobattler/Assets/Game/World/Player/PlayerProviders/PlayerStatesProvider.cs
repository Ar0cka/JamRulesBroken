using Game.World.Player.Interfaces;
using Game.World.Player.StateController;
using UnityEngine;

namespace Game.World.Player.PlayerProviders
{
    public class PlayerStatesProvider : MonoBehaviour, IStateProvider
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

        public bool GetCurrentState(PlayerStates playerStates)
        {
            return _playerStateController.PlayerState[playerStates];
        }
    }

    public struct PlayerProviderData
    {
        public PlayerStateController PlayerStateController;
    }
}