using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpellCollection", menuName = "Config/PlayerSpell", order = 0)]
    public class PlayerSpellCollection : ScriptableObject
    {
        [field:SerializeField] public List<SpellConfig> PlayerStartSpells { get; private set; }
    }
}