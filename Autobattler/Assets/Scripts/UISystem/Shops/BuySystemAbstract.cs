using System;
using Player.Containers;
using ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Shops
{
    public abstract class BuySystemAbstract : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected Button buyButton;
        [SerializeField] protected Button cancelButton;
        
        [SerializeField] protected TransitMoney transitMoney; //TODO Change in service class
        [SerializeField] protected ErrorWindow errorWindow;

        [Header("UI")] 
        [SerializeField] protected Image productImage;
        [SerializeField] protected TextMeshProUGUI productName;
        [SerializeField] protected TextMeshProUGUI productPrice;
        
        protected bool IsOpen() => gameObject.activeSelf;

        /// <summary>
        /// In base realized buy and cancel button listeners and UpdateUI
        /// Buy button -> BuyAction()
        /// Cancel button -> Cancel()
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        /// <param name="onEndBuy"></param>
        /// <typeparam name="TConfig"></typeparam>
        /// <typeparam name="TContainer"></typeparam>
        public virtual void OpenBuyMenu<TConfig, TContainer>(TConfig config, TContainer container, Action<TConfig> onEndBuy)
            where TConfig : class
            where TContainer : IPlayerContainer
        {
            buyButton.onClick.AddListener(BuyAction);
            cancelButton.onClick.AddListener(Cancel);
            
            UpdateUI();
        }

        protected abstract void BuyAction();
        protected abstract void UpdateUI();
        
        public virtual void Cancel()
        {
            errorWindow.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}