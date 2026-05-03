using System.Collections.Generic;
using Player.Containers;
using ScriptableObjects;
using ScriptableObjects.SpellConfigs;
using UnityEngine;

namespace Player
{
    public class PlayerSpellContainer : MonoBehaviour, IPlayerContainer
    {
        [SerializeField] PlayerSpellCollection spellContainer;
        
        private Dictionary<int, SpellConfig> _spellDictionary = new();
        public IReadOnlyDictionary<int, SpellConfig> SpellDictionary => _spellDictionary;
        
        private void Awake()
        {
            foreach (var spell in spellContainer.PlayerStartSpells)
            {
                _spellDictionary.Add(spell.SpellID, spell);
            }
        }
        
        public void AddSpellToContainer(SpellConfig spellConfig)
        {
            if (!_spellDictionary.TryAdd(spellConfig.SpellID, spellConfig)) return;

            _spellDictionary[spellConfig.SpellID] = spellConfig;
        }
        
        public bool ContainsSpell(int spellId) => _spellDictionary.ContainsKey(spellId);
        public List<TContainer> GetContainer<TContainer>()
        {
            throw new System.NotImplementedException();
        }
    }
}