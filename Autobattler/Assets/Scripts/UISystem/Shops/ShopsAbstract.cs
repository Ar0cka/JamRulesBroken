using System.Collections.Generic;
using DefaultNamespace.WorldSceneScripts.NpcDialogScript;
using Player.PlayerProviders;
using Player.StateController;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public abstract class ShopsAbstract<TShopConfig, TDictionaryKey, TShopProduct> : MonoBehaviour, IShop 
        where TShopConfig : ScriptableObject 
        where TShopProduct : ScriptableObject 
    {
        [Header("Data")]
        [SerializeField] private TShopConfig shopConfig;
        [SerializeField] private DialogController dialogWindow;
        [SerializeField] private BuySystemAbstract buySystem;
        
        [Header("UI Components")]
        [SerializeField] private GameObject currentObject;
        [SerializeField] private Button exitButton;
        
        [Header("Transforms")]
        [SerializeField] private Transform cardParent;
        
        private readonly Dictionary<TDictionaryKey, TShopProduct> _shopConfigs = new();
        private readonly List<BuyButton> _buttons = new();

        private IStateProvider _provider;

        private bool _isOpen;

        public virtual void EnterToShop(IStateProvider stateProvider)
        {
            if (_isOpen) return;

            _provider = stateProvider;

            _provider.SwitchPlayerState(PlayerStates.IsDialogWindow, true);
            
            foreach (var unit in shopConfig.UnitConfigs)
            {
                _shopConfigs.TryAdd(unit.config.UnitName, unit);
            }

            foreach (var conf in _shopConfigs.Values)
            {
                var gamePrefab = Instantiate(imagePrefab, cardParent, false);
                var buttonInitialize = gamePrefab.GetComponent<BuyButton>();
                buttonInitialize.Initialize(conf, buyPanel);
                _buttons.Add(buttonInitialize);
            }
            
            exitButton.onClick.AddListener(Exit);
            panelObject.SetActive(true);
            _isOpen = true;
        }

        public void BuyEnd(string unitName, UnitShopConfig changedConfig)
        {
            _shopConfigs[unitName] = changedConfig;

            foreach (var button in _buttons)
            {
                if (_shopConfigs.TryGetValue(button.CurrentUnit, out var value))
                {
                    button.UpdateButtonData(value);
                }
            }
        }

        public void Exit()
        {
            foreach (var button in _buttons)
            {
                button.Destroy();
            }
            
            _buttons.Clear();
            _shopConfigs.Clear();
            
            dialogWindow.CloseDialog();
            panelObject.SetActive(false);

            _provider.SwitchPlayerState(PlayerStates.IsDialogWindow, false);

            if (buyPanel.gameObject.activeInHierarchy)
            {
                buyPanel.ClosePanel();
            }
            
            _isOpen = false;
        }
    }
}