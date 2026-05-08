using System.Collections.Generic;
using Game.Data;
using Game.Data.Player;
using Game.Data.SpellConfigs;
using Game.World.Player.Interfaces;
using UnityEngine;

namespace Game.World.Player
{
    public class PlayerSpellContainer : MonoBehaviour, IPlayerContainer
    {
        [SerializeField] PlayerSpellCollection spellContainer;
        
        private Dictionary<string, SpellConfig> _spellDictionary = new();
        public IReadOnlyDictionary<string, SpellConfig> SpellDictionary => _spellDictionary;
        
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
        
        public bool ContainsSpell(string spellId) => _spellDictionary.ContainsKey(spellId);
        public List<TContainer> GetContainer<TContainer>()
        {
            throw new System.NotImplementedException();
        }
    }
}