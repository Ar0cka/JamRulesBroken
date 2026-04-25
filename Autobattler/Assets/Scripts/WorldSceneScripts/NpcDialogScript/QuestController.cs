using System.Collections.Generic;
using DefaultNamespace.WorldSceneScripts.NpcDialogScript;
using Player;
using ScriptableObjects;
using ShopSystem;
using UnityEngine;

namespace WorldSceneScripts.NpcDialogScript
{
    public class QuestController : MonoBehaviour
    {
        [SerializeField] private GameObject questCardPrefab;
        [SerializeField] private Transform questCardParent;

        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private Wallet playerWallet;
        
        private readonly Dictionary<string, QuestCardUI> _questCards = new();

        private void Awake()
        {
            playerInventory.OnInventoryUpdate += UpdateUI;
        }

        public void AcceptQuest(QuestData data)
        {
            var questCard = Instantiate(questCardPrefab, questCardParent, false);

            var questCardUi = questCard.GetComponent<QuestCardUI>();

            if (questCardUi == null)
            {
                Destroy(questCard);
                return;
            }
            
            var item = playerInventory.GetItemSlot(data.neededItems.ItemName);

            int amount = 0;
            
            if (item is not null && item.Amount > 0)
            {
                amount = item.Amount;
            }
            
            questCardUi.SetQuest(data, playerWallet, amount);
            
            _questCards.Add(data.neededItems.ItemName, questCardUi);
        }

        private void UpdateUI()
        {
            foreach (var quest in _questCards)
            {
                var item = playerInventory.GetItemSlot(quest.Key);

                if (item is null || item.Amount <= 0)
                    continue;
                
                quest.Value.UpdateQuestProgress(item.Amount);
            }
        }

        public void CompleteQuest(string itemName)
        {
            if (_questCards.TryGetValue(itemName, out var value))
            {
                value.CompleteQuest();
            }
        }
    }
}