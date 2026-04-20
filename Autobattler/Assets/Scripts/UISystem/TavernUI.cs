using System;
using System.Collections.Generic;
using Player;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UISystem
{
    public class TavernUI : MonoBehaviour, IShop
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Button exitButton;
        [SerializeField] private ShopConfig shopConfig;
        [SerializeField] private Transform imageParent;
        [SerializeField] private BuyPanelSystem buyPanel;
        private readonly Dictionary<string, UnitShopConfig> _shopConfigs = new();
        private readonly List<BuyButton> _buttons = new();
        
        [SerializeField] private PlayerStateController stateController;

        private bool _isOpen;
        
        public void EnterToShop()
        {
            if (_isOpen) return;

            stateController.IsShopOpen = true;
            
            foreach (var unit in shopConfig.UnitConfigs)
            {
                _shopConfigs.TryAdd(unit.config.UnitName, unit);
            }

            foreach (var conf in _shopConfigs.Values)
            {
                var gamePrefab = Instantiate(imagePrefab, imageParent, false);
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
            
            panelObject.SetActive(false);

            stateController.IsShopOpen = false;

            if (buyPanel.gameObject.activeInHierarchy)
            {
                buyPanel.ClosePanel();
            }
            
            _isOpen = false;
        }
    }
}