using System.Collections.Generic;
using Game.Core.ShopAbstract.Interfaces;
using Game.Core.ShopAbstract.ShopsFactory;
using Game.World.Player.Interfaces;
using Game.World.Player.StateController;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Core.ShopAbstract
{
    
    /// <typeparam name="TShopConfig"></typeparam>
    /// <typeparam name="TShopProduct"></typeparam>
    public abstract class ShopsAbstract<TShopConfig, TShopProduct> : MonoBehaviour, IShop 
        where TShopConfig : ScriptableObject
        where TShopProduct : class
    {
        [Header("Data")]
        [SerializeField] protected TShopConfig shopConfig;
        
        [Header("UI Components")]
        [SerializeField] protected GameObject shopObject;
        [SerializeField] protected Button exitButton;

        [Header("Card Position")] 
        [SerializeField] protected Transform cardParent;
        
        [Inject] protected ShopsCardFactory BuyButtonFactory;
        
        /// <summary>
        /// Using for lisent end buy
        /// </summary>
        protected readonly Dictionary<string, TShopProduct> ShopConfigs = new();
        protected readonly List<BaseBuyButton<TShopProduct>> BuyButtons = new();

        protected IStateProvider StateProvider;
        protected IPlayerContainer PlayerContainer;

        protected bool IsOpen;

        /// <summary>
        /// Enter point for initialize UI panel.
        /// Using InitializeShopCollection in the base realization.
        /// </summary>
        /// <param name="stateProvider"></param>
        /// <param name="playerContainer"></param>
        public virtual void EnterToShop(IStateProvider stateProvider, IPlayerContainer playerContainer)
        {
            if (IsOpen) return;

            StateProvider = stateProvider;
            PlayerContainer = playerContainer;
            
            InitializeShopCollection();
            
            exitButton.onClick.AddListener(Exit);
            
            shopObject.SetActive(true);
            SwitchState(true);
        }

        protected abstract void InitializeShopCollection();
        
        protected void BuyEndBase(string key, TShopProduct product)
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

        protected virtual void Exit()
        {
            foreach (var button in BuyButtons)
            {
                button.Dispose();
            }
            
            BuyButtons.Clear();
            ShopConfigs.Clear();
            
            shopObject.SetActive(false);
            
            SwitchState(false);
        }
    }
}