using System;
using ScriptableObjects;

namespace BattleSystem.UnitSystem.data
{
    public enum EffectType
    {
        Cold,
        Fire,
        Speed,
        Defence,
        None
    }

    [Serializable]
    public class EffectData
    {
        public EffectData(EffectType type, int turns)
        {
            EffectType = type;
            Turns = turns;
        }
        
        public EffectType EffectType;
        public int Turns;
    }
}