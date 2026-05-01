using System.Collections.Generic;
using DefaultNamespace.WorldSceneScripts.NpcDialogScript;
using Player;
using Player.PlayerProviders;
using Player.StateController;
using ScriptableObjects;
using UISystem.ShopButton;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class MagicLavkaUI : ShopsAbstract<SpellShopConfig, SpellConfig>
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject imagePrefab;
        [SerializeField] private Button exitButton;
        [SerializeField] private MagicShopConfig shopConfig;
        [SerializeField] private Transform imageParent;
        [SerializeField] private MagicBuyPanel buyPanel;
        [SerializeField] private DialogController dialogWindow;
        
        private List<SpellShopConfig> _magicList = new();

        protected override void InitializeShopCollection()
        {
            foreach (var spellConfig in _magicList)
            {
                ShopConfigs.TryAdd(spellConfig.config., spellConfig);
            }

            foreach (var conf in ShopConfigs.Values)
            {
                var gamePrefab = Instantiate(imagePrefab, imageParent, false);
                var buttonInitialize = gamePrefab.GetComponent<BaseBuyButton<SpellConfig>>();
                buttonInitialize.Initialize(conf, buyPanel);
                BuyButtons.Add(buttonInitialize);
            }
        }

        public void Exit()
        {
            foreach (var button in BuyButtons)
            {
                button.Destroy();
            }
            
            BuyButtons.Clear();
            ShopConfigs.Clear();
            
            dialogWindow.CloseDialog();
            panelObject.SetActive(false);

            StateProvider.SwitchPlayerState(PlayerStates.IsDialogWindow, false);

            if (buyPanel.gameObject.activeInHierarchy)
            {
                buyPanel.ClosePanel();
            }
            
            _isOpen = false;
        }
    }
}