using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class MagicBuy : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image unitImage;
        [SerializeField] private TextMeshProUGUI priceText;

        private SpellShopConfig _config;
        private MagicBuyPanel _magicBuyPanelSystem;

        public string CurrentSpell => _config.config.SpellName;

        private void Awake()
        {
            button.onClick.AddListener(() =>
            { 
                _magicBuyPanelSystem.gameObject.SetActive(true);
                Debug.Log($"_magic buy panel active = {_magicBuyPanelSystem.gameObject.activeSelf}");
                _magicBuyPanelSystem.OpenPanel(_config);
            });
        }

        public void Initialize(SpellShopConfig config, MagicBuyPanel buyPanelSystem)
        {
            _magicBuyPanelSystem = buyPanelSystem;
            
            _config = config.Clone();
            
            unitImage.sprite = _config.config.SpellIcon;
            priceText.text = _config.price.ToString();
        }
        
        public void UpdateButtonData(SpellShopConfig config)
        {
            _config = config;
            
            unitImage.sprite = _config.config.SpellIcon;
            priceText.text = _config.price.ToString();
        }

        public void Destroy()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}