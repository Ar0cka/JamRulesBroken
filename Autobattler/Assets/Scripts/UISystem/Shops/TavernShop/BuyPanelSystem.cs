using System;
using System.Globalization;
using Player;
using ScriptableObjects;
using ShopSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class BuyPanelSystem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI endPrice;
        [SerializeField] private TextMeshProUGUI count;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TavernUI tavernUI;
        [SerializeField] private Slider countSlider;

        [SerializeField] private TransitMoney transitMoney;
        [SerializeField] private GameObject errorMessage;

        [SerializeField] private PlayerUnitContainer playerContainer;
        
        private UnitShopConfig _config;

        private bool _isOpen = false;

        private void BuyItem()
        {
            int buyCount = (int) countSlider.value;
            var priceAll = _config.price * buyCount;
            var isSuccesses = transitMoney.PlayerBuy(priceAll);
            
            if (isSuccesses)
            {
                _config.count -= buyCount;
                playerContainer.AddUnit(new PlayerUnit
                {
                    unitConfig = _config.config,
                    unitCount = buyCount
                });
            }
            
            if (!isSuccesses)
            {
                errorMessage.SetActive(true);
                return;
            }
            
            ClosePanel();
        }

        public void OpenPanel(UnitShopConfig config)
        {
            if (_isOpen) return;
            
            _config = config;

            countSlider.onValueChanged.AddListener(OnSliderValueChanged);
            
            countSlider.maxValue = _config.count;
            countSlider.value = 1;
            
            image.sprite = _config.config.UnitSprite;
            
            buyButton.onClick.AddListener(BuyItem);
            cancelButton.onClick.AddListener(ClosePanel);
            
            _isOpen = true;
        }

        public void ClosePanel()
        {
            tavernUI.BuyEnd(_config.config.UnitName, _config);
            gameObject.SetActive(false);
            
            buyButton.onClick.RemoveListener(BuyItem);
            cancelButton.onClick.RemoveListener(ClosePanel);
            countSlider.onValueChanged.RemoveAllListeners();
            
            _isOpen = false;
        }

        private void OnSliderValueChanged(float value)
        {
            count.text = $"{(int)countSlider.value}";
            endPrice.text = $"End price: {Mathf.CeilToInt(_config.price * value)}";
        }
    }
}