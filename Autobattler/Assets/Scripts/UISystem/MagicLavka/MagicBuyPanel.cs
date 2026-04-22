using Player;
using ScriptableObjects;
using ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class MagicBuyPanel : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private MagicLavkaUI magicLavka;

        [SerializeField] private TransitMoney transitMoney;
        [SerializeField] private ErrorMessage errorMessage;

        [SerializeField] private PlayerSpellContainer playerContainer;
        
        private SpellShopConfig _config;
        
        private bool _isOpen = false;

        private void BuyItem()
        {
            if (playerContainer.ContainsSpell(_config.config.SpellName))
            {
                errorMessage.OpenPanel(ErrorType.SpellType);
                
                return;
            }
            
            var isSuccesses = transitMoney.PlayerBuyUnit(_config.price);
            
            if (isSuccesses)
            {
                playerContainer.AddSpellToContainer(_config.config);
            }
            else
            {
                errorMessage.OpenPanel(ErrorType.MoneyType);
                return;
            }
            
            ClosePanel();
        }

        public void OpenPanel(SpellShopConfig config)
        {
            if (_isOpen) return;
            
            _config = config;
            
            image.sprite = _config.config.SpellIcon;
            price.text = $"Cost: {_config.price.ToString()}";
            
            buyButton.onClick.AddListener(BuyItem);
            cancelButton.onClick.AddListener(ClosePanel);

            _isOpen = true;
        }

        public void ClosePanel()
        {
            magicLavka.BuyEnd(_config.config.SpellName, _config);
            gameObject.SetActive(false);
            
            buyButton.onClick.RemoveListener(BuyItem);
            cancelButton.onClick.RemoveListener(ClosePanel);
            
            _isOpen = false;
        }
    }
}