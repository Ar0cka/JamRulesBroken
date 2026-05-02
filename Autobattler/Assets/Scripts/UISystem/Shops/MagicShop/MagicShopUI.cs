using Player;
using ScriptableObjects;
using UISystem.Shops;
using UISystem.Shops.ShopsFactory;
using UnityEngine;

namespace UISystem.MagicLavka
{
    public class MagicShopUI : ShopsAbstract<MagicShopConfig, SpellShopConfig>
    {
        [SerializeField] private MagicShopBuySystem buySystem;
        
        protected override void InitializeShopCollection()
        {
            var list = shopConfig.Clone();
            
            foreach (var config in list)
            {
                ShopConfigs[config.config.SpellID] = config;
            }

            foreach (var conf in ShopConfigs.Values)
            {
                var buttonInitialize =
                    BuyButtonFactory.CreateBuyButton(ShopCardType.MagicCard, conf, cardParent,
                        buySystem, PlayerContainer);
                
                BuyButtons.Add(buttonInitialize);
            }
        }
    }
}