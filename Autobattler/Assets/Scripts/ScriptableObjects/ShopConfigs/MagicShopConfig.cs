using System;
using System.Collections.Generic;
using ScriptableObjects.SpellConfigs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MagicShopConfig", menuName = "Config/MagicShop", order = 0)]
    public class MagicShopConfig : ScriptableObject
    {
        [SerializeField] private List<SpellShopConfig> shopConfigs;

        public List<SpellShopConfig> Clone()
        {
            List<SpellShopConfig> spells = new();

            foreach (var shop in shopConfigs)
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
}