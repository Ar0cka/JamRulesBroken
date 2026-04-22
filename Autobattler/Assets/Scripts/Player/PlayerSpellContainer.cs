using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerSpellContainer : MonoBehaviour
    {
        [SerializeField] PlayerSpellCollection spellContainer;
        
        private Dictionary<string, SpellConfig> _spellDictionary = new();
        public IReadOnlyDictionary<string, SpellConfig> SpellDictionary => _spellDictionary;
        
        private void Awake()
        {
            foreach (var spell in spellContainer.PlayerStartSpells)
            {
                _spellDictionary.Add(spell.SpellName, spell);
            }
        }
        
        public void AddSpellToContainer(SpellConfig spellConfig)
        {
            if (!_spellDictionary.TryAdd(spellConfig.SpellName, spellConfig)) return;

            _spellDictionary[spellConfig.SpellName] = spellConfig;
        }
        
        public bool ContainsSpell(string spellName) => _spellDictionary.ContainsKey(spellName);
    }
}