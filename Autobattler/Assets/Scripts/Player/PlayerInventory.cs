using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<string, InventorySlot> _playerInventory = new();
        public Action OnInventoryUpdate;

        public void AddItemToInventory(ItemConfig itemConfig)
        {
            if (_playerInventory.TryGetValue(itemConfig.ItemName, out var slot))
            {
                slot.Amount += 1;
                OnInventoryUpdate?.Invoke();
                return;
            }
            
            var inventorySlot = new InventorySlot()
            {
                Config = itemConfig,
                Amount = 1
            };

            _playerInventory[inventorySlot.Config.ItemName] = inventorySlot;

            OnInventoryUpdate?.Invoke();
        }

        public bool RemoveItem(ItemConfig itemConfig, int amountToRemove)
        {
            if (_playerInventory.TryGetValue(itemConfig.ItemName, out var slot))
            {
                if (slot.Amount < amountToRemove)
                    return false;

                slot.Amount -= amountToRemove;
                return true;
            }

            return false;
        }
        
        public InventorySlot GetItemSlot(string itemConfigName)
        {
            if (_playerInventory.TryGetValue(itemConfigName, out var slot))
                return slot;

            return null;
        }
    }

    public class InventorySlot
    {
        public ItemConfig Config;
        public int Amount;
    }
    
}