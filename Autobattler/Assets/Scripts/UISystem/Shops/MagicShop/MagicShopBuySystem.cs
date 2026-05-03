using System;
using Player;
using Player.Containers;
using ScriptableObjects;
using UISystem.MagicLavka;
using UnityEngine;

namespace UISystem.Shops.MagicShop
{
    public class MagicShopBuySystem : BuySystemAbstract
    {
        [SerializeField] private MagicShopUI magicShop;

        private Action<int, SpellShopConfig> _endBuy;
        private SpellShopConfig _currentConfig;
        private PlayerSpellContainer _spellContainer;

        public override void InitializeListener(Action<int, object> onEndFunc)
        {
            _endBuy = onEndFunc;
        }

        public override void OpenBuyMenu(object config, IPlayerContainer container)
        {
            if (IsOpen())
                return;

            _currentConfig = (SpellShopConfig)config;
            _spellContainer = (PlayerSpellContainer)container;
            
            base.OpenBuyMenu(config, container);
        }

        protected override void BuyAction()
        {
            if (_spellContainer.ContainsSpell(_currentConfig.config.SpellID))
            {
                errorWindow.OpenPanel(ErrorType.SpellType);
                return;
            }
            
            var isSuccesses = transitMoney.PlayerBuy(_currentConfig.price);
            
            if (!isSuccesses)
            {
                errorWindow.OpenPanel(ErrorType.MoneyType);
                return;
            }
            
            _spellContainer.AddSpellToContainer(_currentConfig.config);
            _endBuy?.Invoke(_currentConfig.config.SpellID, _currentConfig);
            
            Cancel();
        }

        protected override void UpdateUI()
        {
            productImage.sprite = _currentConfig.config.SpellVisualData.spellIcon;
            productName.text = $"Spell name: {_currentConfig.config.SpellData.spellName}";
            productPrice.text = $"Cost: {_currentConfig.price.ToString()}";
        }

        public override void Cancel()
        {
            _currentConfig = null;
            _spellContainer = null;
            
            base.Cancel();
        }
    }
}