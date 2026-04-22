using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MagicBook
{
    public class MagicBookUI : MonoBehaviour
    {
        [SerializeField] private PlayerSpellContainer spellContainer;
        [SerializeField] private GameObject spellCardPrefab;
        [SerializeField] private Transform spellCardsContainer;
        [SerializeField] private Button closeButton;
        
        private List<GameObject> _spellCards = new();

        private bool isOpen = false;
        
        public void OpenBook()
        {
            if (isOpen) return;
            
            var spells = spellContainer.SpellDictionary;

            foreach (var spell in spells.Values)
            {
                var spellUi = Instantiate(spellCardPrefab, spellCardsContainer, false);
                
                var cardManager = spellUi.GetComponent<MagicBookCard>();
                
                cardManager.Init(spell);
                _spellCards.Add(spellUi);
            }
            
            closeButton.onClick.AddListener(CloseBook);
        }

        private void CloseBook()
        {
            foreach (var card in _spellCards)
            {
                Destroy(card);
            }
            
            _spellCards.Clear();

            gameObject.SetActive(false);
            
            closeButton.onClick.RemoveAllListeners();
        }
    }
}