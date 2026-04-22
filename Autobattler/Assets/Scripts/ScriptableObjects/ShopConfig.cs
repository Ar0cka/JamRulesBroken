using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TaverConfig", menuName = "Config/Tavern", order = 0)]
    public class ShopConfig : ScriptableObject
    {
        [field:SerializeField] public List<UnitShopConfig> UnitConfigs { get; private set; }

        public List<UnitShopConfig> CloneConfigs()
        {
            var newList = new List<UnitShopConfig>();
        
            foreach (var unitConfig in UnitConfigs)
            {
                newList.Add(unitConfig.Clone()); // Вызываем Clone() каждого элемента
            }
        
            return newList;
        }
    }

    [CreateAssetMenu(fileName = "MagicShopConfig", menuName = "Config/MagicShop", order = 0)]
    public class MagicShopConfig : ScriptableObject
    {
        [SerializeField] private List<SpellShopConfig> _shopConfigs;

        public List<SpellShopConfig> Clone()
        {
            List<SpellShopConfig> spells = new();

            foreach (var shop in _shopConfigs)
            {
                spells.Add(shop.Clone());
            }
            
            return spells;
        }
    }
    
    [Serializable]
    public class SpellShopConfig
    {
        public SpellConfig config;
        public int price;
        
        public SpellShopConfig Clone()
        {
            return new SpellShopConfig()
            {
                config = config,
                price = price,
            };
        }
    }
    
    [Serializable]
    public class UnitShopConfig
    {
        public UnitConfigs config;
        public int price;
        public int count;

        public UnitShopConfig Clone()
        {
            return new UnitShopConfig()
            {
                config = config,
                price = price,
                count = count
            };
        }
    }
}