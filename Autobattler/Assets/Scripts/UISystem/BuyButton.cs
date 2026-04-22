using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image unitImage;
        [SerializeField] private TextMeshProUGUI priceText;

        private UnitShopConfig _config;
        private BuyPanelSystem _buyPanelSystem;

        public string CurrentUnit => _config.config.UnitName;
        
        public void Initialize(UnitShopConfig config, BuyPanelSystem buyPanelSystem)
        {
            _buyPanelSystem = buyPanelSystem;
            
            _config = config.Clone();
            
            unitImage.sprite = _config.config.UnitSprite;
            countText.text = _config.count.ToString();
            priceText.text = _config.price.ToString();
            
            button.onClick.AddListener(() =>
            {
                _buyPanelSystem.gameObject.SetActive(true);
                _buyPanelSystem.OpenPanel(_config);
            });
        }
        
        public void UpdateButtonData(UnitShopConfig config)
        {
            _config = config;
            
            unitImage.sprite = _config.config.UnitSprite;
            countText.text = _config.count.ToString();
            priceText.text = _config.price.ToString();
        }

        public void Destroy()
        {
            button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}