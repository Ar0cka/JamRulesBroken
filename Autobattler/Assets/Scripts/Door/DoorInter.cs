using System;
using Player;
using ScriptableObjects;
using UISystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using WorldSceneScripts.NpcDialogScript;

namespace DefaultNamespace.Door
{
    public class DoorInter : MonoBehaviour
    {
        [SerializeField] private EventPanelSettings eventPanel;
        [SerializeField] private ErrorWindow errorWindow;
        [SerializeField] private QuestController questController;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private TilemapCollider2D tilemapCollider2D;
        [SerializeField] private CompositeCollider2D compositeCollider2D;
        
        //TODO Initialize boss scene
        
        [SerializeField] private QuestData questData;
        [SerializeField] private string questAcceptText;
        [SerializeField] private string questCompleteText;
        [SerializeField] private string notNeededItemText;
        [SerializeField] private BossScene bossScene;
        
        private bool _isPlayer = false;
        private bool _isPutButton = false;
        private bool _isQuestTake = false;
        private bool _isQuestCompleted = false;

        private void Update()
        {
            if (_isPlayer && Input.GetKeyDown(KeyCode.E) && !_isPutButton)
            {
                _isPutButton = true;

                try
                {
                    if (!_isQuestTake)
                        QuestNonTake();
                    
                    if (_isQuestTake && !_isQuestCompleted)
                    {
                        QuestIsAccept();
                    }
                    
                    if (_isQuestCompleted)
                        errorWindow.OpenPanel(ErrorType.MoneyType, questCompleteText);
                }
                finally
                {
                    _isPutButton = false;
                }
            }
        }

        private void QuestCompleted()
        {
            tilemapCollider2D.enabled = false;
            compositeCollider2D.enabled = false;
            questController.CompleteQuest(questData.neededItems.ItemName);
            StartCoroutine(bossScene.SpawnBossScene());
            errorWindow.OpenPanel(ErrorType.MoneyType, questCompleteText);
        }

        private void QuestNonTake()
        {
            _isQuestTake = true;
            questController.AcceptQuest(questData);
        }

        private void QuestIsAccept()
        {
            if (CheckItemInInventory())
            {
                _isQuestCompleted = true;
                QuestCompleted();
            }

            var playerItem = playerInventory.GetItemSlot(questData.neededItems.ItemName);
            
            int amount = playerItem?.Amount ?? 0;

            notNeededItemText = $"Find all keys: {amount}/{questData.neededAmount}";
            
            errorWindow.OpenPanel(ErrorType.MoneyType, notNeededItemText);
        }

        private bool CheckItemInInventory()
        {
            var item = playerInventory.GetItemSlot(questData.neededItems.ItemName);

            if (item == null || item.Amount < questData.neededAmount)
                return false;

            var deleted = playerInventory.RemoveItem(questData.neededItems, questData.neededAmount);

            if (deleted)
                return true;

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayer = true;
                eventPanel.SetEventText("Input E for open door");
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayer = false;
                eventPanel.gameObject.SetActive(false);
            }
        }
    }
}