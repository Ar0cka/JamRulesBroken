using System;
using System.Globalization;
using Player;
using Player.Containers;
using ScriptableObjects;
using ShopSystem;
using TMPro;
using UISystem.Shops;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class TavernBuySystem : BuySystemAbstract
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI count;
        [SerializeField] private Slider countSlider;
        
        [Header("Components")]
        [SerializeField] private TavernUI tavernUI;

        private PlayerUnitContainer _playerContainer;
        private Action<int, UnitShopConfig>_onEndBuy;
        private UnitShopConfig _config;

        public override void OpenBuyMenu(object config, IPlayerContainer container)
        {
            if (IsOpen())
                return;
            
            _config = (UnitShopConfig)config;
            _playerContainer = (PlayerUnitContainer)container;
            
            base.OpenBuyMenu(config, container);
        }

        private void OnSliderValueChanged(float value)
        {
            count.text = $"{(int)countSlider.value}";
            productPrice.text = $"End price: {Mathf.CeilToInt(_config.price * value)}";
        }

        public override void InitializeListener(Action<int, object> onEndFunc)
        {
            _onEndBuy = onEndFunc;
        }

        protected override void BuyAction()
        {
            int buyCount = (int) countSlider.value;
            var priceAll = _config.price * buyCount;
            var isSuccesses = transitMoney.PlayerBuy(priceAll);
            
            if (!isSuccesses)
            {
                errorWindow.OpenPanel(ErrorType.MoneyType, "Failed to buy units, not enough money");
                return;
            }
            
            _config.count -= buyCount;
            _playerContainer.AddUnit(new PlayerUnit
            {
                unitConfig = _config.config,
                unitCount = buyCount
            });

            _onEndBuy?.Invoke(_config.config.UnitID, _config);
            
            Cancel();
        }

        protected override void UpdateUI()
        {
            productImage.sprite = _config.config.VisualData.unitSprite;
            productName.text = _config.config.UnitData.unitName;
            
            countSlider.onValueChanged.AddListener(OnSliderValueChanged);
            countSlider.maxValue = _config.count;
            countSlider.value = 1;
            
            count.text = $"{(int)countSlider.value}";
            productPrice.text = $"Price: {Mathf.CeilToInt(_config.price * countSlider.value)}";
        }
    }
}