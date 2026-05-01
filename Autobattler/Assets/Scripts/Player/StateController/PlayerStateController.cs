using System;
using System.Collections.Generic;
using Player.StateController;
using UnityEngine;

namespace Player
{
    public class PlayerStateController
    {
        public Dictionary<PlayerStates, bool> PlayerStates = new();
        
        /// <summary>
        /// Updating player target player state;
        /// </summary>
        /// <param name="playerState"></param>
        /// <param name="currentState"></param>
        public void UpdatePlayerState(PlayerStates playerState, bool currentState)
        {
            PlayerStates[playerState] = currentState;
        }
    }
}