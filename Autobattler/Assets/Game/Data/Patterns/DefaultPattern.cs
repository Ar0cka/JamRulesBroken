using System;
using System.Collections.Generic;
using Game.Data.UnitConfigs;
using UnityEngine;

namespace Game.Data.Patterns
{
    [Serializable]
    public abstract class DefaultPattern
    {
        [field: SerializeField] public string PatternID { get; private set; }
        [field: SerializeField] public string PatternName { get; private set; }

        [field: SerializeField] public List<EffectConfig> PatternEffects { get; private set; }
        [field: SerializeField] public List<UnitType> EffectiveTargets { get; private set; }

        [field: SerializeField] public int Cooldown { get; private set; }
        
        [field: SerializeField] public PatternType PatternType { get; private set; }
    }
    
    public enum PatternType
    {
        General, 
        Sample,
        Unit
    }
}