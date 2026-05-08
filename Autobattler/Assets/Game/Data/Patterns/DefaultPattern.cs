using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Patterns
{
    public abstract class DefaultPattern : ScriptableObject
    {
        [Header("Pattern data")]
        [field: SerializeField] public string PatternId { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        [Header("Buffs/Debuffs")]
        [field: SerializeField] public List<string> Buffs { get; private set; }
        [field: SerializeField] public List<string> Debuffs { get; private set; }
        
        [Header("Pattern cooldown")]
        [field: SerializeField] public int Cooldown { get; private set; }
    }
}