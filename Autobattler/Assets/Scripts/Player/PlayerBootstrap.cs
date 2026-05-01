using System;
using Player.PlayerProviders;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Button exitButton;
        [SerializeField] private ShopConfig shopConfig;
        [SerializeField] private Transform imageParent;
        [SerializeField] private DialogController dialogWindow;
        [SerializeField] private BuyPanelSystem buyPanel;
        private readonly Dictionary<string, UnitShopConfig> _shopConfigs = new();
        private readonly List<BuyButton> _buttons = new();

        private IStateProvider _provider;

        private bool _isOpen;

        public void EnterToShop(IStateProvider stateProvider)
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