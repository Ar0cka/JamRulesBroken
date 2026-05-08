using Game.Core.ShopAbstract;
using Game.Data.ShopConfigs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.World.Shops.MagicShop
{
    public class MagicBuy : BaseBuyButton<SpellShopConfig>
    {
        [SerializeField] private Image unitImage;
        [SerializeField] private TextMeshProUGUI priceText;

        protected override void UpdateUI()
        {
            unitImage.sprite = Config.config.SpellVisualData.spellIcon;
            priceText.text = $"Price: {Config.price}";
        }

        public override void Dispose()
        {
            base.Dispose();
            Destroy(gameObject);
        }
    }
}