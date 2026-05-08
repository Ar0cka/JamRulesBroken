using Game.Core.ShopAbstract;
using Game.Core.ShopAbstract.ShopsFactory;
using Game.Data.ShopConfigs;
using Game.World.Player.Interfaces;
using UnityEngine;

namespace Game.World.Shops.TavernShop
{
    public class TavernUI : ShopsAbstract<ShopConfig, UnitShopConfig>
    {
        [SerializeField] private TavernBuySystem tavernBuySystem;

        private IStateProvider _provider;

        private bool _isOpen;

        protected override void InitializeShopCollection()
        {
            tavernBuySystem.InitializeListener((i, o) =>
            {
                BuyEndBase(i, (UnitShopConfig)o);
            });

            var list = shopConfig.CloneConfigs();
            
            foreach (var unit in list)
            {
                ShopConfigs[unit.config.UnitID] = unit;
            }
            
            foreach (var conf in ShopConfigs.Values)
            {
                var buttonInitialize =
                    BuyButtonFactory.CreateBuyButton(ShopCardType.TavernCard, conf, 
                        cardParent, tavernBuySystem, PlayerContainer);
                
                BuyButtons.Add(buttonInitialize);
            }
        }

        protected override void Exit()
        {
            base.Exit();
            
            if (tavernBuySystem.gameObject.activeSelf)
            {
                tavernBuySystem.Cancel();
            }
        }
    }
}