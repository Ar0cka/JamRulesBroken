using Player;
using Player.PlayerProviders;
using ScriptableObjects;
using UISystem.Shops.Interfaces;
using UISystem.Shops.ShopsFactory;
using UnityEngine;

namespace UISystem.Shops.TavernShop
{
    public class TavernUI : ShopsAbstract<ShopConfig, UnitShopConfig>, IShop
    {
        [SerializeField] private TavernBuySystem tavernBuySystem;

        [SerializeField] private PlayerUnitContainer playerContainer;

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
                        cardParent, tavernBuySystem, playerContainer);
                
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