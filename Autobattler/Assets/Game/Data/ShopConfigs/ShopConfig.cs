using System;
using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.ShopConfigs
{
    [CreateAssetMenu(fileName = "TavernConfig", menuName = "Config/Tavern", order = 0)]
    public class ShopConfig : ScriptableObject
    {
        [field:SerializeField] private List<UnitShopConfig> unitConfigs;

        public List<UnitShopConfig> CloneConfigs()
        {
            var newList = new List<UnitShopConfig>();
        
            foreach (var unitConfig in unitConfigs)
            {
                newList.Add(unitConfig.Clone()); // Вызываем Clone() каждого элемента
            }
        
            return newList;
        }
    }
    
    [Serializable]
    public class UnitShopConfig
    {
        public UnitConfig config;
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