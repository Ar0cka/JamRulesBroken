using System.Collections.Generic;
using ScriptableObjects;

namespace Player
{
    public class PlayerInventory
    {
        private Dictionary<string, InventorySlot> _playerInventory = new();

        public void AddItemToInventory(ItemConfig itemConfig)
        {
            if (_playerInventory.TryGetValue(itemConfig.name, out var slot))
            {
                slot.Amount += 1;
                return;
            }
            
            var inventorySlot = new InventorySlot()
            {
                Config = itemConfig,
                Amount = 1
            };

            _playerInventory[inventorySlot.Config.ItemName] = inventorySlot;
        }

        public bool RemoveItem(ItemConfig itemConfig, int amountToRemove)
        {
            if (_playerInventory.TryGetValue(itemConfig.name, out var slot))
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