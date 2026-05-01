using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
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
}