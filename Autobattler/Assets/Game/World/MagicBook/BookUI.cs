using System.Collections.Generic;
using Game.Data.SpellConfigs;
using Game.World.Player.PlayerProviders;
using Game.World.Player.StateController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Game.World.MagicBook
{
    public class BookUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI spellName;
        [SerializeField] private TextMeshProUGUI spellDescription;
        [SerializeField] private TextMeshProUGUI spellType;
        [SerializeField] private TextMeshProUGUI spellDamage;

        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private Image spellImage;
        [SerializeField] private PlayerStatesProvider stateController;

        private List<SpellConfig> _spells = new();
        private bool _isOpen = false;
        private int _currentPage = 0;

        private void Update()
        {
            nextButton.interactable = NextPageAvailable();
            previousButton.interactable = PreviousPageAvailable();
        }

        public void Open(List<SpellConfig> playerSpells)
        {
            if (_isOpen) return;
            
            _spells = playerSpells;
            _isOpen = true;
            stateController.SwitchPlayerState(PlayerStates.IsBook, true);
            gameObject.SetActive(true);

            closeButton.onClick.AddListener(Close);
            nextButton.onClick.AddListener(ChangePageForward);
            previousButton.onClick.AddListener(ChangePageBack);

            var currentSpell = _spells[_currentPage];
            UpdateUI(currentSpell);
        }

        private void UpdateUI(SpellConfig currentSpell)
        {
            spellName.text = $"Spell name: {currentSpell.SpellData.spellName}";
            spellDescription.text = $"Description: {currentSpell.SpellData.description}";
            spellImage.sprite = currentSpell.SpellVisualData.spellIcon;
            spellType.text = $"Spell type: {currentSpell.SpellData.spellType}";
            spellDamage.text = $"Damage: {currentSpell.SpellStats.spellDamage}";
        }

        private void Close()
        {
            closeButton.onClick.RemoveAllListeners();
            nextButton.onClick.RemoveAllListeners();
            previousButton.onClick.RemoveAllListeners();
            _spells.Clear();
            _currentPage = 0;
            _isOpen = false;
            stateController.SwitchPlayerState(PlayerStates.IsBook, false);
            gameObject.SetActive(false);
        }

        private void ChangePageBack()
        {
            _currentPage--;
            UpdateUI(_spells[_currentPage]);
        }

        private void ChangePageForward()
        {
            _currentPage++;
            UpdateUI(_spells[_currentPage]);
        }

        private bool NextPageAvailable()
        {
            return _currentPage + 1 < _spells.Count;
        }

        private bool PreviousPageAvailable()
        {
            return _currentPage - 1 >= 0;
        }
    }
}