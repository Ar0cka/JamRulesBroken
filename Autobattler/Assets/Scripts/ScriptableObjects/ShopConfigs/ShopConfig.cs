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