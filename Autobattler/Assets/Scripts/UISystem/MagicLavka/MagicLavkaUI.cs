using System.Collections.Generic;
using Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class MagicLavkaUI : MonoBehaviour, IShop
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Button exitButton;
        [SerializeField] private MagicShopConfig shopConfig;
        [SerializeField] private Transform imageParent;
        [SerializeField] private MagicBuyPanel buyPanel;
        private readonly Dictionary<string, SpellShopConfig> _shopConfigs = new();
        private readonly List<MagicBuy> _buttons = new();
        
        [SerializeField] private PlayerStateController stateController;

        private bool _isOpen;
        private List<SpellShopConfig> _magicList = new();
        
        public void EnterToShop()
        {
            if (_isOpen) return;

            stateController.IsShopOpen = true;

            _magicList = shopConfig.Clone();
            
            foreach (var spellConfig in _magicList)
            {
                _shopConfigs.TryAdd(spellConfig.config.SpellName, spellConfig);
            }

            foreach (var conf in _shopConfigs.Values)
            {
                var gamePrefab = Instantiate(imagePrefab, imageParent, false);
                var buttonInitialize = gamePrefab.GetComponent<MagicBuy>();
                buttonInitialize.Initialize(conf, buyPanel);
                _buttons.Add(buttonInitialize);
            }
            
            exitButton.onClick.AddListener(Exit);
            panelObject.SetActive(true);
            _isOpen = true;
        }

        public void BuyEnd(string unitName, SpellShopConfig changedConfig)
        {
            _shopConfigs[unitName] = changedConfig;

            foreach (var button in _buttons)
            {
                if (_shopConfigs.TryGetValue(button.CurrentSpell, out var value))
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