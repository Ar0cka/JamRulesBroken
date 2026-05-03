using System;
using Player.PlayerProviders;
using Player.StateController;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerProvider playerProvider;

        public void Awake()
        {
            InitializeStateController();
        }

        private void InitializeStateController()
        {
            playerProvider.InitializePlayerProvider(new PlayerProviderData
            {
                PlayerStateController = new PlayerStateController(),
            });
        }
    }
}