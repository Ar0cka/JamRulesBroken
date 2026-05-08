using Game.Core.ShopAbstract;
using Game.Core.ShopAbstract.ShopsFactory;
using Game.Data.ShopConfigs;
using UnityEngine;

namespace Game.World.Shops.MagicShop
{
    public class MagicShopUI : ShopsAbstract<MagicShopConfig, SpellShopConfig>
    {
        [SerializeField] private MagicShopBuySystem buySystem;
        
        protected override void InitializeShopCollection()
        {
            var list = shopConfig.Clone();
            
            buySystem.InitializeListener((i, o) =>
            {
                BuyEndBase(i, (SpellShopConfig)o);
            });
            
            foreach (var config in list)
            {
                ShopConfigs[config.config.SpellID] = config;
            }

            foreach (var conf in ShopConfigs.Values)
            {
                Debug.Log($"Create card: {conf.config.SpellData.spellName}");
                
                var buttonInitialize =
                    BuyButtonFactory.CreateBuyButton(ShopCardType.MagicCard, conf, cardParent,
                        buySystem, PlayerContainer);
                
                BuyButtons.Add(buttonInitialize);
            }
        }

        protected override void Exit()
        {
            if (buySystem.gameObject.activeSelf)
                buySystem.Cancel();
            
            base.Exit();
        }
    }
}