using System.Collections.Generic;

namespace Player.StateController
{
    public class PlayerStateController
    {
        public Dictionary<PlayerStates, bool> PlayerState = new Dictionary<PlayerStates, bool>()
        {
            { PlayerStates.IsDialogWindow , false},
            { PlayerStates.IsWalking , false},
            { PlayerStates.IsBook, false}
        };
        
        /// <summary>
        /// Updating player target player state;
        /// </summary>
        /// <param name="playerState"></param>
        /// <param name="currentState"></param>
        public void UpdatePlayerState(PlayerStates playerState, bool currentState)
        {
            PlayerState[playerState] = currentState;
        }
    }
}