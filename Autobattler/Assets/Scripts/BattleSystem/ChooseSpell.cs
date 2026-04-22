using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem
{
    public class ChooseSpell : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject spellBookPrefab;
        [SerializeField] private GameObject spellCardPrefab;
        [SerializeField] private Transform spellParent;

        [SerializeField] private ErrorMessage errorMessage;

        [SerializeField] private PlayerCastSystem playerCastSystem;
     
        private List<SpellCardSettings> _spellChooseButtons = new();
        
        
        public void Initialize(List<SpellConfig> spells)
        {
            if (spellBookPrefab.activeSelf)
                return;
            
            Action<string> chooseSpell = (spellName) =>
            {
                if (!playerCastSystem.IsCanCast)
                {
                    errorMessage.OpenPanel( ErrorType.SpellType,"Failed to cast spell. You dont have spell action");
                }
                
                playerCastSystem.SetSpellName(spellName);
                Exit();
            };
            
            exitButton.onClick.AddListener(Exit);
            
            foreach (var spell in spells)
            {
                var spellCard = Instantiate(spellCardPrefab, spellParent, false);
                spellCard.GetComponent<SpellCardSettings>().Initialize(spell.SpellName, spell.SpellIcon, chooseSpell);
                _spellChooseButtons.Add(spellCard.GetComponent<SpellCardSettings>());
            }
        }

        public void EnablePanel()
        {
            if (spellBookPrefab.activeSelf)
                return;

            spellBookPrefab.SetActive(true);
        }

        private void Exit()
        {
            spellBookPrefab.SetActive(false);
        }
        
        public void Dispose()
        {
            foreach (var spellCard in _spellChooseButtons)
            {
                spellCard.Dispose();
            }
            
            _spellChooseButtons.Clear();
            
            exitButton.onClick.RemoveAllListeners();
        }
    }
}