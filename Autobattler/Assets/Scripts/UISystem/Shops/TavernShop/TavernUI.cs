using System;
using System.Collections.Generic;
using DefaultNamespace.WorldSceneScripts.NpcDialogScript;
using Player;
using Player.PlayerProviders;
using Player.StateController;
using ScriptableObjects;
using TMPro;
using UISystem.Shops;
using UISystem.Shops.Interfaces;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UISystem
{
    public class TavernUI : ShopsAbstract<ShopConfig, UnitShopConfig>, IShop
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Button exitButton;
        [SerializeField] private ShopConfig shopConfig;
        [SerializeField] private Transform imageParent;
        [SerializeField] private DialogController dialogWindow;
        [SerializeField] private TavernBuySystem tavernBuy;
        private readonly Dictionary<int, UnitShopConfig> _shopConfigs = new();
        private readonly List<BuyButton> _buttons = new();

        private IStateProvider _provider;

        private bool _isOpen;

        public void EnterToShop(IStateProvider stateProvider)
        {
            if (_isOpen) return;

            _provider = stateProvider;

            _provider.SwitchPlayerState(PlayerStates.IsDialogWindow, true);
            
            tavernBuy.InitializeListener((i, o) =>
            {
                BuyEnd(i, (UnitShopConfig)o);
            });
            
            foreach (var unit in shopConfig.UnitConfigs)
            {
                _shopConfigs.TryAdd(unit.config.UnitName, unit);
            }

            foreach (var conf in _shopConfigs.Values)
            {
                var gamePrefab = Instantiate(imagePrefab, imageParent, false);
                var buttonInitialize = gamePrefab.GetComponent<BuyButton>();
                buttonInitialize.Initialize(conf, tavernBuy);
                _buttons.Add(buttonInitialize);
            }
            
            exitButton.onClick.AddListener(Exit);
            panelObject.SetActive(true);
            _isOpen = true;
        }

        public void BuyEnd(int unitId, UnitShopConfig changedConfig)
        {
            _shopConfigs[unitId] = changedConfig;

            foreach (var button in _buttons)
            {
                if (_shopConfigs.TryGetValue(button.CurrentProductID, out var value))
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

            if (tavernBuy.gameObject.activeInHierarchy)
            {
                tavernBuy.ClosePanel();
            }
            
            _isOpen = false;
        }
    }
}