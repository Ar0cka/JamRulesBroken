using System.Collections.Generic;
using Game.Data.SpellConfigs;
using UnityEngine;

namespace Game.Data.Player
{
    [CreateAssetMenu(fileName = "SpellCollection", menuName = "Config/PlayerSpell", order = 0)]
    public class PlayerSpellCollection : ScriptableObject
    {
        [field:SerializeField] public List<SpellConfig> PlayerStartSpells { get; private set; }
    }
}