using System;
using System.Collections;
using Player;
using ScriptableObjects;
using ScriptableObjects.WorldSceneConfigs;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.BossFight
{
    public class BeginFight : MonoBehaviour
    {
        [SerializeField] private BossPanel bossPanel;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private PlayerFightAnimation playerFight;
        [SerializeField] private PlayerStateController playerStateController;

        [SerializeField] private EnemyWorldSceneConfig enemyConfig;
        [SerializeField] private ItemConfig flower;
        private bool _isPlayer;

        private void CheckPlayerInventory()
        {
            var itemSlot = playerInventory.GetItemSlot(flower.ItemName);

            if (itemSlot == null)
            {
                StartCoroutine(playerFight.StartFight(enemyConfig, gameObject));
                return;
            }

            if (itemSlot.Amount > 0)
            {
                playerStateController.IsDialogWindow = true;
                bossPanel.OpenPanel(enemyConfig, gameObject);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_isPlayer)
            {
                _isPlayer = true;
                CheckPlayerInventory();
            }
        }
    }
}