using System;
using Player;
using ScriptableObjects;
using ShopSystem;
using TMPro;
using UISystem.MagicLavka;
using UISystem.Shops;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class MagicShopBuySystem : BuySystemAbstract
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private MagicShopUI magicShop;
        
        private void BuyItem()
        {
            if (playerContainer.ContainsSpell(_config.config.SpellName))
            {
                errorWindow.OpenPanel(ErrorType.SpellType);
                
                return;
            }
            
            var isSuccesses = transitMoney.PlayerBuy(_config.price);
            
            if (isSuccesses)
            {
                playerContainer.AddSpellToContainer(_config.config);
            }
            else
            {
                errorWindow.OpenPanel(ErrorType.MoneyType);
                return;
            }
            
            ClosePanel();
        }

        public void ClosePanel()
        {
            magicShop.BuyEnd(_config.config.SpellName, _config);
            gameObject.SetActive(false);
            
            buyButton.onClick.RemoveListener(BuyItem);
            cancelButton.onClick.RemoveListener(ClosePanel);
        }

        public override void OpenBuyMenu<TConfig, TContainer>(TConfig config, 
            TContainer playerContainer, Action<TConfig> onEndBuy)
        {
            if (IsOpen())
                return;

            var shopConfig = config as SpellShopConfig;

            if (shopConfig is null)
                throw new NullReferenceException(nameof(SpellShopConfig));
            
            
            
            base.OpenBuyMenu(config, playerContainer, onEndBuy);
        }

        protected override void BuyAction()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateUI()
        {
            image.sprite = _config.config.SpellIcon;
            price.text = $"Cost: {_config.price.ToString()}";
        }
    }
}