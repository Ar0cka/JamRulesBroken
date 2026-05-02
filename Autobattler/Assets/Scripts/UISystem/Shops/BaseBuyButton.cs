using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.ShopButton
{
    public abstract class BaseBuyButton<TConfig> : MonoBehaviour, IDisposable
    {
        [SerializeField] protected Button buyButton;
        
        public int CurrentProductID { get; private set; }
        
        protected IBuySystem BuySystem { get; private set; }
        protected TConfig Config { get; private set; }

        public virtual void Initialize(TConfig conf, IBuySystem buySystem)
        {
            BuySystem = buySystem;
            Config = conf;
            
            UpdateUI();
            
            buyButton.onClick.AddListener(buySystem.OpenBuyMenu);
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