using System;
using Game.World.MoneySystem;
using Game.World.Player.Interfaces;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.ShopAbstract
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

        public abstract void InitializeListener(Action<string, object> action);
        /// <summary>
        /// In base realized buy and cancel button listeners and UpdateUI
        /// Buy button -> BuyAction()
        /// Cancel button -> Cancel()
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        public virtual void OpenBuyMenu(object config, IPlayerContainer container)
        {
            buyButton.onClick.AddListener(BuyAction);
            cancelButton.onClick.AddListener(Cancel);
            
            gameObject.SetActive(true);
            
            UpdateUI();
        }

        protected abstract void BuyAction();
        protected abstract void UpdateUI();
        public virtual void Cancel()
        {
            errorWindow.gameObject.SetActive(false);
            gameObject.SetActive(false);
            
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.RemoveAllListeners();
        }
    }
}