using System;
using Player.Containers;
using ScriptableObjects;
using UISystem.Shops;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.ShopButton
{
    public abstract class BaseBuyButton<TConfig> : MonoBehaviour, IDisposable
        where TConfig : class
    {
        [SerializeField] protected Button buyButton;
        
        protected TConfig Config { get; private set; }
        protected BuySystemAbstract BuySystem { get; private set; }
        
        public int CurrentProductID { get; private set; }

        public virtual void Initialize(TConfig conf, IPlayerContainer playerContainer, BuySystemAbstract buySystem)
        {
            BuySystem = buySystem;
            Config = conf;
            
            UpdateUI();
            
            buyButton.onClick.AddListener(() =>
            {
                BuySystem.OpenBuyMenu(conf, playerContainer);
            });
        }

        protected abstract void UpdateUI();
        
        public void UpdateButtonData(TConfig config)
        {
            Config = config;
            UpdateUI();
        }

        public virtual void Dispose()
        {
            BuySystem = null;
            Config = default;
            
            buyButton.onClick.RemoveAllListeners();
        }
    }
}