using Game.World.Player.PlayerProviders;
using Game.World.Player.StateController;
using UnityEngine;

namespace Game.World.Player
{
    public class PlayerBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerStatesProvider playerStatesProvider;

        public void Awake()
        {
            InitializeStateController();
        }

        private void InitializeStateController()
        {
            playerStatesProvider.InitializePlayerProvider(new PlayerProviderData
            {
                PlayerStateController = new PlayerStateController(),
            });
        }
    }
}