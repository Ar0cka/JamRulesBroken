using System;
using System.Collections.Generic;
using Player.PlayerProviders;
using Player.StateController;
using UISystem.ShopButton;
using UISystem.Shops.ShopsFactory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem.Shops
{
    
    /// <typeparam name="TShopConfig"></typeparam>
    /// <typeparam name="TShopProduct"></typeparam>
    public abstract class ShopsAbstract<TShopConfig, TShopProduct> : MonoBehaviour, IShop where TShopConfig : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] protected TShopConfig shopConfig;
        [SerializeField] protected BuySystemAbstract buySystem;
        
        [Header("UI Components")]
        [SerializeField] protected GameObject shopObject;
        [SerializeField] protected Button exitButton;

        [Header("Card Position")] 
        [SerializeField] protected Transform cardParent;
        
        [Inject] protected ShopsCardFactory BuyButtonFactory;
        
        /// <summary>
        /// Using for lisent end buy
        /// </summary>
        protected Action<int, TShopProduct> OnBuyEnd;
        protected readonly Dictionary<int, TShopProduct> ShopConfigs = new();
        protected readonly List<BaseBuyButton<TShopProduct>> BuyButtons = new();

        protected IStateProvider StateProvider;

        protected bool IsOpen;

        /// <summary>
        /// Enter point for initialize UI panel.
        /// Using InitializeShopCollection in the base realization.
        /// </summary>
        /// <param name="stateProvider"></param>
        public virtual void EnterToShop(IStateProvider stateProvider)
        {
            if (IsOpen) return;

            StateProvider = stateProvider;
            
            InitializeShopCollection();
            
            exitButton.onClick.AddListener(Exit);
            OnBuyEnd = BuyEndBase;
            
            shopObject.SetActive(true);
            SwitchState(true);
        }

        protected abstract void InitializeShopCollection();
        
        protected void BuyEndBase(int key, TShopProduct product)
        {
            ShopConfigs[key] = product;
            
            foreach (var button in BuyButtons)
            {
                if (ShopConfigs.TryGetValue(button.CurrentProductID, out var value))
                {
                    button.UpdateButtonData(value);
                }
            }
        }

        protected void SwitchState(bool isOpen)
        {
            IsOpen = isOpen;
            StateProvider.SwitchPlayerState(PlayerStates.IsDialogWindow, isOpen);
        }

        public void Exit()
        {
            foreach (var button in BuyButtons)
            {
                button.Dispose();
            }
            
            BuyButtons.Clear();
            ShopConfigs.Clear();
            
            shopObject.SetActive(false);

            if (buySystem.gameObject.activeInHierarchy)
            {
                buySystem.CloseBuyMenu();
            }
            
            SwitchState(false);
        }
    }
}