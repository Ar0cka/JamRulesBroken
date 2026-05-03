using ScriptableObjects;
using TMPro;
using UISystem.ShopButton;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Shops.TavernShop
{
    public class BuyButton : BaseBuyButton<UnitShopConfig>
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image unitImage;

        protected override void UpdateUI()
        {
            unitImage.sprite = Config.config.VisualData.unitSprite;
            countText.text = Config.count.ToString();
            priceText.text = Config.price.ToString();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            Destroy(gameObject);
        }
    }
}